using Nexmo.Api.Request;

namespace Nexmo.Api.Verify
{
    public interface IVerifyClient
    {
        /// <summary>
        /// Use Verify request to generate and send a PIN to your user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        VerifyResponse VerifyRequest(VerifyRequest request, Credentials creds = null);

        /// <summary>
        /// Use Verify check to confirm that the PIN you received from your user matches the one sent by Nexmo in your Verify request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        VerifyCheckResponse VerifyCheck(VerifyCheckRequest request, Credentials creds = null);

        /// <summary>
        /// Use Verify search to check the status of past or current verification requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        VerifySearchResponse VerifySearch(VerifySearchRequest request, Credentials creds = null);

        /// <summary>
        /// Control the progress of your Verify requests. To cancel an existing Verify request, or to trigger the next verification event
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        VerifyControlResponse VerifyControl(VerifyControlRequest request, Credentials creds = null);

        /// <summary>
        /// Use Verify request to generate and send a PIN to your user to authorize a payment: 
        /// 1. Create a request to send a verification code to your user. 
        /// 2. Check the status field in the response to ensure that your request was successful (zero is success). 
        /// 3. Use the request_id field in the response for the Verify check.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        VerifyResponse VerifyRequestWithPSD2(Psd2Request request, Credentials creds = null);
    }
}