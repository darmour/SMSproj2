using System;

namespace SMSproj2
{
    public class Utils
    {
        public static string ExtractApiChatGatewayUrl(string resourceConnectionString)
        {
            var uri = new Uri(resourceConnectionString.Replace("endpoint=", string.Empty, StringComparison.OrdinalIgnoreCase));
            return $"{uri.Scheme}://{uri.Host}";
        }
    }
}
