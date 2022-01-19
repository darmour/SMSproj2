//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//

using Newtonsoft.Json;
#nullable disable
namespace Microsoft.Azure.Communication.Email.SharedClients.Models
{
    public class Attachment
    {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("contentType", Required = Required.Always)]
        public AttachmentType ContentType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size", Required = Required.Always)]
        public int Size { get; set; }

        [JsonProperty("contentBytes", Required = Required.Always)]
        public string ContentBytes { get; set; }
    }
}