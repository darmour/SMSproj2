using Azure.Communication.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Communication.Email.SharedClients.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace SMSproj2.Models
{
    public class WebEmail
    {
        public  string toRecipients { get; set; }  = "";
        public  string subject { get; set; }  = "";
        public  string emailMsg { get; set; }  = "";

        public  string status { get; set; } = "";

        
        private string fromSender { get; } = ""; 

        private string connectionString { get;  } = ""; 


        private string restApiUri { get; } = ""; 

        public WebEmail()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            IConfigurationRoot configuration = configurationBuilder.Build();

            fromSender = configuration["EmailFromAddress"];
            connectionString = configuration["EmailConnectionString"];
 
            //Use the Utils class that was part of the Chat hero sample
            restApiUri = Utils.ExtractApiChatGatewayUrl(connectionString);
        }

        public  void sendWebMail()
        {
            CallSendMail().GetAwaiter().GetResult();
        }

        private  async Task CallSendMail()
        {

            try
            {
                var mailFromAddress = fromSender;
                var ccRecipients = "";
                var bccRecipients = "";
                var replyTo = "";

                //var subject = "Testing the privte preview of ACS email";
                var body = emailMsg;

                var identityAndTokenResponse = await GenerateSkypeToken(connectionString, new[] { CommunicationTokenScope.VoIP });

                var emailMessage = CreateEmailMessage(mailFromAddress, toRecipients, ccRecipients, bccRecipients, replyTo, subject, body);

                var response = await SendEmail(new Uri(restApiUri), emailMessage, identityAndTokenResponse.AccessToken.Token, false);

                StringBuilder debugString = new StringBuilder();
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    debugString.AppendLine("Send failed, Date: " + response.Headers.Date);
                }
                else
                {
                    debugString.AppendLine("Send success, Date: " + response.Headers.Date);
                }

                if (response.Headers.TryGetValues("x-ms-request-id", out IEnumerable<string> messageIds))
                {
                    foreach (var messageId in messageIds)
                    {
                        debugString.AppendLine("MessageId: " + messageId);
                    }
                }
                debugString.AppendLine("StatusCode: " + response.StatusCode.ToString());
                debugString.AppendLine("MailFromAddress: " + mailFromAddress.ToString() + "\nToRecipients: " + toRecipients.ToString());
                if (!string.IsNullOrEmpty(ccRecipients))
                {
                    debugString.AppendLine("ccRecipients: " + ccRecipients.ToString());
                }
                if (!string.IsNullOrEmpty(bccRecipients))
                {
                    debugString.AppendLine("BccRecipients: " + bccRecipients.ToString());
                }
                if (!string.IsNullOrEmpty(replyTo))
                {
                    debugString.AppendLine("ReplyTo: " + replyTo.ToString());
                }
                if (!string.IsNullOrEmpty(responseBody))
                {
                    debugString.AppendLine("Body: " + responseBody);
                }
                status = debugString.ToString();
                //Console.WriteLine(debugString.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private  async Task<CommunicationUserIdentifierAndToken> GenerateSkypeToken(string connectionString, IEnumerable<CommunicationTokenScope> scopes)
        {
            var client = new CommunicationIdentityClient(connectionString);
            var identityAndTokenResponse = await client.CreateUserAndTokenAsync(scopes);

            return identityAndTokenResponse.Value;
        }

        private  EmailMessageDto CreateEmailMessage(string fromEmailAddress, string toRecipients, string ccRecipients, string bccRecipients, string replyTo, string subject, string body)
        {
            var emailDto = new EmailMessageDto();

            // Email Content
            emailDto.Content = new EmailContent();
            emailDto.Content.Body = new EmailBody
            {
                PlainText = body
            };
            emailDto.Content.Subject = subject;

            emailDto.Sender = new EmailAddress
            {
                Email = fromEmailAddress,
                DisplayName = $"{fromEmailAddress} - (Sender)"
            };

            // Email Recipients
            emailDto.Recipients = new EmailRecipients();

            // To Recipients
            if (!string.IsNullOrWhiteSpace(toRecipients))
            {
                emailDto.Recipients.ToRecipients = new List<EmailAddress>();
                foreach (string emailaddress in toRecipients.Split(';'))
                {
                    if (!string.IsNullOrWhiteSpace(emailaddress))
                    {
                        emailDto.Recipients.ToRecipients.Add(new EmailAddress
                        {
                            Email = emailaddress,
                            DisplayName = $"{emailaddress} - (Receiver)"
                        });
                    }
                }
            }

            // CC Recipients
            if (!string.IsNullOrWhiteSpace(ccRecipients))
            {
                emailDto.Recipients.CcRecipients = new List<EmailAddress>();
                foreach (string emailaddress in ccRecipients.Split(';'))
                {
                    if (!string.IsNullOrWhiteSpace(emailaddress))
                    {
                        emailDto.Recipients.CcRecipients.Add(new EmailAddress
                        {
                            Email = emailaddress,
                            DisplayName = $"{emailaddress} - (Receiver)"
                        });
                    }
                }
            }

            // BCC Recipients
            if (!string.IsNullOrWhiteSpace(bccRecipients))
            {
                emailDto.Recipients.BccRecipients = new List<EmailAddress>();
                foreach (string emailaddress in bccRecipients.Split(';'))
                {
                    if (!string.IsNullOrWhiteSpace(emailaddress))
                    {
                        emailDto.Recipients.BccRecipients.Add(new EmailAddress
                        {
                            Email = emailaddress,
                            DisplayName = $"{emailaddress} - (Receiver)"
                        });
                    }
                }
            }

            // Reply To
            if (!string.IsNullOrWhiteSpace(replyTo))
            {
                emailDto.ReplyTo = new List<EmailAddress>();
                foreach (var emailAddress in replyTo.Split(";"))
                {
                    if (!string.IsNullOrWhiteSpace(emailAddress))
                    {
                        emailDto.ReplyTo.Add(new EmailAddress
                        {
                            Email = emailAddress,
                            DisplayName = $"{emailAddress} - (ReplyTo)"
                        });
                    }
                }
            }

            return emailDto;
        }

        private  async Task<HttpResponseMessage> SendEmail(Uri emailEndpoint, EmailMessageDto emailMessageDto, string skypeToken, bool skipSslValidation)
        {
            var jsonPayload = JsonConvert.SerializeObject(emailMessageDto, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var content = new StringContent(
                jsonPayload,
                Encoding.UTF8,
                "application/json");

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(emailEndpoint, $"email/SendQueue?api-version=2021-10-01-preview"),
                Content = content
            };

            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", skypeToken);
            httpRequestMessage.Headers.Add("Repeatability-Request-Id", Guid.NewGuid().ToString());
            httpRequestMessage.Headers.Add("Repeatability-First-Sent", DateTime.UtcNow.ToString("r"));

            HttpClientHandler clientHandler = new HttpClientHandler();
            if (skipSslValidation)
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            }

            var httpClient = new HttpClient(clientHandler);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }

}

