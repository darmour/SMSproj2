//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Microsoft.Azure.Communication.Email.SharedClients.Models
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EmailImportance
    {
        [EnumMember]
        Normal = 0,

        [EnumMember]
        Low = 1,

        [EnumMember]
        High = 2
    }
}