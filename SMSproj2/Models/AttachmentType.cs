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
    public enum AttachmentType
    {
        [EnumMember]
        Pdf = 0,

        [EnumMember]
        Docx = 1,

        [EnumMember]
        Txt = 2
    }
}