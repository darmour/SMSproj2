//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//

using Microsoft.Azure.Communication.Email.SharedClients.Exceptions;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
#nullable disable
namespace Microsoft.Azure.Communication.Email.SharedClients.Models
{
    public class EmailAddress
    {
        [JsonProperty("email", Required = Required.Always)]
        [EmailAddress]
        public string Email { get; set; }

       
        [JsonProperty("displayName", DefaultValueHandling = DefaultValueHandling.Populate, Required = Required.DisallowNull)]
        [DefaultValue("")]
        public string DisplayName { get; set; }

        public void ValidateEmailAddress()
        {
            MailAddress mailAddress = ToMailAddress();

            var hostParts = mailAddress.Host.Trim().Split(".");
            if (hostParts.Count() < 2)
            {
                throw new InvalidEmailAddressException($"Invalid format for email address host: {mailAddress.Host}");
            }
        }

        private MailAddress ToMailAddress()
        {
            MailAddress emailAddress;
            try
            {
                emailAddress = new MailAddress(Email);
            }
            catch (Exception ex)
            {
                throw new InvalidEmailAddressException($"Invalid format for email address: {Email} {ex.ToString()}");
            }

            return emailAddress;
        }

        public string GetUserFromEmailAddress()
        {
            MailAddress mailAddress = ToMailAddress();

            return mailAddress.User;
        }

        public string GetHostFromEmailAddress()
        {
            MailAddress mailAddress = ToMailAddress();
            return mailAddress.Host;
        }

        public override bool Equals(object other)
        {
            return Equals(other as EmailAddress);
        }

        public bool Equals(EmailAddress other)
        {
            return other != null
                && other.Email.Equals(Email, StringComparison.OrdinalIgnoreCase)
                && other.DisplayName.Equals(DisplayName, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(
                Email,
                DisplayName).GetHashCode();
        }

        public static bool operator ==(EmailAddress lhs, object rhs) =>
            ReferenceEquals(lhs, rhs) || (!(lhs is null) && !(rhs is null) && lhs.Equals(rhs));

        public static bool operator !=(EmailAddress lhs, object rhs) => !(lhs == rhs);

    }
}