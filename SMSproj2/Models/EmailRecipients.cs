//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
#nullable disable
namespace Microsoft.Azure.Communication.Email.SharedClients.Models
{
    public class EmailRecipients
    {
        [JsonProperty("toRecipients", Required = Required.Always)]
        public IList<EmailAddress> ToRecipients { get; set; }

        [JsonProperty("ccRecipients")]
        public IList<EmailAddress> CcRecipients { get; set; }

        [JsonProperty("bccRecipients")]
        public IList<EmailAddress> BccRecipients { get; set; }

        public override bool Equals(object other)
        {
            return Equals(other as EmailRecipients);
        }

        public bool Equals(EmailRecipients other)
        {
            return other != null
                && AreListsEqual(other.ToRecipients, ToRecipients)
                && AreListsEqual(other.CcRecipients, CcRecipients)
                && AreListsEqual(other.BccRecipients, BccRecipients);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(
                ToRecipients,
                CcRecipients,
                BccRecipients).GetHashCode();
        }

        public static bool operator ==(EmailRecipients lhs, object rhs) =>
            ReferenceEquals(lhs, rhs) || (!(lhs is null) && !(rhs is null) && lhs.Equals(rhs));

        public static bool operator !=(EmailRecipients lhs, object rhs) => !(lhs == rhs);

        private bool AreListsEqual(IList<EmailAddress> list1, IList<EmailAddress> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            foreach (var emailAddress in list1)
            {
                if (!list2.Contains(emailAddress))
                {
                    return false;
                }
            }

            return true;
        }

    }
}
