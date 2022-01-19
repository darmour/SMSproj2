//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
#nullable disable
namespace Microsoft.Azure.Communication.Email.SharedClients.Models
{
    public class EmailContent
    {
        [JsonProperty("subject", Required = Required.Always)]
        public string Subject { get; set; }

        [JsonProperty("body", Required = Required.Always)]
        public EmailBody Body { get; set; }

        [JsonProperty("importance")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EmailImportance Importance { get; set; }

        public override bool Equals(object other)
        {
            return Equals(other as EmailContent);
        }

        public bool Equals(EmailContent other)
        {
            return other != null
                && other.Subject.Equals(Subject, StringComparison.OrdinalIgnoreCase)
                && other.Body == Body
                && other.Importance == Importance;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(
                Subject,
                Body,
                Importance).GetHashCode();
        }

        public static bool operator ==(EmailContent lhs, object rhs) =>
            ReferenceEquals(lhs, rhs) || (!(lhs is null) && !(rhs is null) && lhs.Equals(rhs));

        public static bool operator !=(EmailContent lhs, object rhs) => !(lhs == rhs);
    }
}