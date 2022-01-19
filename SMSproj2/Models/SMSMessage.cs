using System;
using System.Collections.Generic;
using System.Configuration;
using Azure;
using Azure.Communication;
using Azure.Communication.Sms;
#nullable disable
namespace SMSproj2.Models
{
    public class SMSMessage
    {
        public string phoneNumber { get; set; } = "";
        public string smsMessageText { get; set; } = "";

        public string status { get; set; } = "";
        private string fromSender { get; } = ""; // Your E.164 formatted from phone number used to send SMS
        private string connectionString { get; } = ""; // Find your Communication Services resource in the Azure portal

        public SMSMessage()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            IConfigurationRoot configuration = configurationBuilder.Build();

            fromSender = configuration["SMSfromSender"];
            connectionString = configuration["SMSConnectionString"];

        }

        public void sendMessage()
        {
            SmsClient smsClient = new SmsClient(connectionString);
            SmsSendResult sendResult = smsClient.Send(
                from: fromSender, // Your E.164 formatted from phone number used to send SMS
                to: phoneNumber, // E.164 formatted recipient phone number
                message: smsMessageText);
            if(sendResult.Successful == true)
            {
                status = "The mesaage was sent successfully";
            }
            else
            {
                status = "The message was unable to be sent";
            }
            //status = "Message id {sendResult.Successful}"; 
            //return "Message id {sendResult.MessageId}";
        }

    }
}
