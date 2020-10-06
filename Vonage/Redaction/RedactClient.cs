using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Redaction
{
    public class RedactClient : IRedactClient
    {
        public Credentials Credentials { get; set; }

        public RedactClient(Credentials creds = null)
        {
            Credentials = creds;
        }


        public async Task<bool> RedactAsync(RedactRequest request, Credentials creds = null)
        {
            await ApiRequest.DoRequestWithJsonContentAsync<object>
            (
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api,"/v1/redact/transaction"),
                request,
                ApiRequest.AuthType.Basic,
                creds??Credentials
            );
            return true;
        }
    }
}