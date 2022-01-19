using Newtonsoft.Json;
using System;
#nullable disable
namespace Microsoft.Azure.Communication.Email.SharedClients.Models
{
    public class EmailBody
    {
        [JsonProperty("plainText")]
        public string PlainText { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        public override bool Equals(object other)
        {
            return Equals(other as EmailBody);
        }

        public bool Equals(EmailBody other)
        {
            return other != null
                && other.PlainText.Equals(PlainText, StringComparison.OrdinalIgnoreCase)
                && other.Html.Equals(Html, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(
                PlainText,
                Html).GetHashCode();
        }

        public static bool operator ==(EmailBody lhs, object rhs) =>
            ReferenceEquals(lhs, rhs) || (!(lhs is null) && !(rhs is null) && lhs.Equals(rhs));

        public static bool operator !=(EmailBody lhs, object rhs) => !(lhs == rhs);
    }
}
