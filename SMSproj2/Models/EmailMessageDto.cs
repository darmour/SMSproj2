//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//

using Microsoft.Azure.Communication.Email.SharedClients.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
#nullable disable
namespace Microsoft.Azure.Communication.Email.SharedClients.Models
{
    public class EmailMessageDto
    {
        [JsonProperty("headers")]
        public IList<EmailCustomHeader> Headers { get; set; }

        [JsonProperty("sender", Required = Required.Always)]
        public EmailAddress Sender { get; set; }

        [JsonProperty("content", Required = Required.Always)]
        public EmailContent Content { get; set; }

        [JsonProperty("recipients", Required = Required.Always)]
        public EmailRecipients Recipients { get; set; }

        [JsonProperty("attachments")]
        public IList<Attachment> Attachments { get; set; }

        [JsonProperty("replyTo")]
        public IList<EmailAddress> ReplyTo { get; set; }

        [JsonProperty("feedbackEmail")]
        public IList<EmailAddress> FeedbackEmail { get;set;}
    }
}
