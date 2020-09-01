using Nexmo.Api.Request;

namespace Nexmo.Api.Redaction
{
    [System.Obsolete("The Nexmo.Api.Redaction.RedactClient class is obsolete. " +
        "References to it should be switched to the new Vonage.Redaction.RedactClient class.")]
    public class RedactClient : IRedactClient
    {
        public Credentials Credentials { get; set; }

        public RedactClient(Credentials creds = null)
        {
            Credentials = creds;
        }


        public bool Redact(RedactRequest request, Credentials creds = null)
        {
            ApiRequest.DoRequestWithJsonContent<object>
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