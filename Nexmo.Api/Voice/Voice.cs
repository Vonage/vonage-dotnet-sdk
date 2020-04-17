using Nexmo.Api.Request;
using System;
using System.IO;

namespace Nexmo.Api.Voice
{
    public class Voice
    {
        public const string POST = "POST";
        public const string DELETE = "DELETE";
        public const string PUT = "PUT";
        public const string CALLS_ENDPOINT = "v1/calls";
        
        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static Call.CallResponse CreateCall(CallCommand command, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<Call.CallResponse>(POST, ApiRequest.GetBaseUriFor(typeof(Call), CALLS_ENDPOINT), command, ApiRequest.AuthType.Bearer, creds);
        }

        /// <summary>
        /// GET /v1/calls - retrieve information about all your Calls
        /// <param name="filter">Filter to search calls on</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// </summary>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static PaginatedResponse<Call.CallList> GetCalls(CallSearchFilter filter, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<PaginatedResponse<Call.CallList>>(ApiRequest.GetBaseUriFor(typeof(Call), CALLS_ENDPOINT), ApiRequest.AuthType.Bearer, filter, creds);
        }

        /// <summary>
        /// GET /v1/calls/{uuid} - retrieve information about a single Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static Call.CallResponse GetCall(string id, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<Call.CallResponse>(ApiRequest.GetBaseUriFor(typeof(Call), $"{CALLS_ENDPOINT}/{id}"), ApiRequest.AuthType.Bearer, credentials: creds);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid} - modify an existing Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static Call.CallResponse UpdateCall(string id, CallEditCommand command, Credentials creds = null) 
        {            
            return ApiRequest.DoRequestWithJsonContent<Call.CallResponse>(PUT, ApiRequest.GetBaseUriFor(typeof(Call), $"{CALLS_ENDPOINT}/{id}"), command, ApiRequest.AuthType.Bearer, creds);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/stream - stream an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static CallCommandResponse StartStream(string id, StreamCommand command, Credentials creds)
        {
            return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>(PUT, ApiRequest.GetBaseUriFor(typeof(Call), $"{CALLS_ENDPOINT}/{id}/stream"), command, ApiRequest.AuthType.Bearer, creds);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/stream - stop streaming an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static CallCommandResponse StopStream(string id, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>(DELETE, ApiRequest.GetBaseUriFor(typeof(Call), $"{CALLS_ENDPOINT}/{id}/stream"), new { }, ApiRequest.AuthType.Bearer, creds);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/talk - send a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static CallCommandResponse StartTalk(string id, TalkCommand cmd, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>(PUT, ApiRequest.GetBaseUriFor(typeof(Call), $"{CALLS_ENDPOINT}/{id}/talk"), cmd, ApiRequest.AuthType.Bearer, creds);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static CallCommandResponse StopTalk(string id, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>(DELETE, ApiRequest.GetBaseUriFor(typeof(Call), $"{CALLS_ENDPOINT}/{id}/talk"), new { }, ApiRequest.AuthType.Bearer, creds);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public static CallCommandResponse StartDtmf(string id, DtmfCommand cmd, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>(PUT, ApiRequest.GetBaseUriFor(typeof(Call), $"{CALLS_ENDPOINT}/{id}/dtmf"), cmd, ApiRequest.AuthType.Bearer, creds);
        }

        public static GetRecordingResponse GetRecording(string recordingUrl, Credentials creds = null)
        {
            using (var response = ApiRequest.DoGetRequestWithJwt(new Uri(recordingUrl), creds))
            {
                var readTask = response.Content.ReadAsStreamAsync();
                byte[] bytes;
                readTask.Wait();
                using (var ms = new MemoryStream())
                {
                    readTask.Result.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                return new GetRecordingResponse()
                {
                    ResultStream = bytes,
                    Status = response.StatusCode
                };
            }
        }
        
    }
}
