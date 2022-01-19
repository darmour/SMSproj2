//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//

using Newtonsoft.Json;
using System;
#nullable disable
namespace Microsoft.Azure.Communication.Email.SharedClients.Models
{
    public class EmailCustomHeader
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("value", Required = Required.AllowNull)]
        public string Value { get; set; }

        public override bool Equals(object other)
        {
            return Equals(other as EmailCustomHeader);
        }

        public bool Equals(EmailCustomHeader other)
        {
            return other != null
                && other.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)
                && other.Value.Equals(Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(
                Name,
                Value).GetHashCode();
        }

        public static bool operator ==(EmailCustomHeader lhs, object rhs) =>
            ReferenceEquals(lhs, rhs) || (!(lhs is null) && !(rhs is null) && lhs.Equals(rhs));

        public static bool operator !=(EmailCustomHeader lhs, object rhs) => !(lhs == rhs);
    }
}