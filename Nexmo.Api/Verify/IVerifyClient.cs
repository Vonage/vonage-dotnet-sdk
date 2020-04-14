using Nexmo.Api.Request;

namespace Nexmo.Api.Verify
{
    public interface IVerifyClient
    {
        VerifyResponse VerifyRequest(VerifyRequest request, Credentials creds = null);

        VerifyCheckResponse VerifyCheck(VerifyCheckRequest request, Credentials creds = null);

        VerifySearchResponse VerifySearch(VerifySearchRequest request, Credentials creds = null);

        VerifyControlResponse VerifyControl(VerifyControlRequest request, Credentials creds = null);        
    }
}