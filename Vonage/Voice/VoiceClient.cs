using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;
using System;
using System.IO;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;

namespace Vonage.Voice
{
    public class VoiceClient : IVoiceClient
    {
        public const string POST = "POST";
        public const string DELETE = "DELETE";
        public const string PUT = "PUT";
        public const string CALLS_ENDPOINT = "v1/calls";
        public Credentials Credentials { get; set; }

        public VoiceClient(Credentials credentials = null)
        {
            Credentials = credentials;
        }

        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="command"></param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Task<CallResponse> CreateCallAsync(CallCommand command, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<CallResponse>(
                POST,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CALLS_ENDPOINT),
                command,
                ApiRequest.AuthType.Bearer,
                creds ?? Credentials
                );
        }

        /// <summary>
        /// GET /v1/calls - retrieve information about all your Calls
        /// <param name="filter">Filter to search calls on</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// </summary>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Task<PageResponse<CallList>> GetCallsAsync(CallSearchFilter filter, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<PageResponse<CallList>>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CALLS_ENDPOINT),
                ApiRequest.AuthType.Bearer,
                filter,
                creds ?? Credentials
                );
        }

        /// <summary>
        /// GET /v1/calls/{uuid} - retrieve information about a single Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Task<CallRecord> GetCallAsync(string id, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<CallRecord>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}"),
                ApiRequest.AuthType.Bearer,
                credentials: creds ?? Credentials
            );
        }

        /// <summary>
        /// PUT /v1/calls/{uuid} - modify an existing Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public async Task<bool> UpdateCallAsync(string id, CallEditCommand command, Credentials creds = null)
        {
            await ApiRequest.DoRequestWithJsonContentAsync<CallRecord>(
                PUT,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}"),
                command,
                ApiRequest.AuthType.Bearer,
                creds ?? Credentials
            );
            return true;
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/stream - stream an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Task<CallCommandResponse> StartStreamAsync(string id, StreamCommand command, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<CallCommandResponse>(
                PUT,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}/stream"),
                command,
                ApiRequest.AuthType.Bearer,
                creds ?? Credentials
            );
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/stream - stop streaming an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Task<CallCommandResponse> StopStreamAsync(string id, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<CallCommandResponse>(
                DELETE,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}/stream"),
                new { },
                ApiRequest.AuthType.Bearer,
                creds ?? Credentials
            );
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/talk - send a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Task<CallCommandResponse> StartTalkAsync(string id, TalkCommand cmd, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<CallCommandResponse>(
                PUT,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}/talk"),
                cmd,
                ApiRequest.AuthType.Bearer,
                creds ?? Credentials
            );
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Task<CallCommandResponse> StopTalkAsync(string id, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<CallCommandResponse>(
                DELETE,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}/talk"),
                new { },
                ApiRequest.AuthType.Bearer,
                creds ?? Credentials
            );
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Task<CallCommandResponse> StartDtmfAsync(string id, DtmfCommand cmd, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<CallCommandResponse>(
                PUT,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}/dtmf"),
                cmd,
                ApiRequest.AuthType.Bearer,
                creds ?? Credentials
                );
        }

        /// <summary>
        /// GET - retrieves the recording from a call based off of the input url
        /// </summary>
        /// <param name="recordingUrl">Url where the recorded call lives</param>
        /// <param name="creds">Overridden credentials</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <returns>A response containing a byte array representing the file stream</returns>
        public async Task<GetRecordingResponse> GetRecordingAsync(string recordingUrl, Credentials creds = null)
        {
            using (var response = await ApiRequest.DoGetRequestWithJwtAsync(new Uri(recordingUrl), creds ?? Credentials))
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

        public CallResponse CreateCall(CallCommand command, Credentials creds = null)
        {
            return CreateCallAsync(command, creds).GetAwaiter().GetResult();
        }

        public PageResponse<CallList> GetCalls(CallSearchFilter filter, Credentials creds = null)
        {
            return GetCallsAsync(filter, creds).GetAwaiter().GetResult();
        }

        public CallRecord GetCall(string id, Credentials creds = null)
        {
            return GetCallAsync(id, creds).GetAwaiter().GetResult();
        }

        public bool UpdateCall(string id, CallEditCommand command, Credentials creds = null)
        {
            return UpdateCallAsync(id, command, creds).GetAwaiter().GetResult();
        }

        public CallCommandResponse StartStream(string id, StreamCommand command, Credentials creds = null)
        {
            return StartStreamAsync(id, command, creds).GetAwaiter().GetResult();
        }

        public CallCommandResponse StopStream(string id, Credentials creds = null)
        {
            return StopStreamAsync(id, creds).GetAwaiter().GetResult();
        }

        public CallCommandResponse StartTalk(string id, TalkCommand cmd, Credentials creds = null)
        {
            return StartTalkAsync(id, cmd, creds).GetAwaiter().GetResult();
        }

        public CallCommandResponse StopTalk(string id, Credentials creds = null)
        {
            return StopTalkAsync(id, creds).GetAwaiter().GetResult();
        }

        public CallCommandResponse StartDtmf(string id, DtmfCommand cmd, Credentials creds = null)
        {
            return StartDtmfAsync(id, cmd, creds).GetAwaiter().GetResult();
        }

        public GetRecordingResponse GetRecording(string recordingUrl, Credentials creds = null)
        {
            return GetRecordingAsync(recordingUrl, creds).GetAwaiter().GetResult();
        }

        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="toNumber"></param>
        /// <param name="fromNumber"></param>
        /// <param name="ncco"></param>
        /// <returns></returns>
        public Task<CallResponse> CreateCallAsync(string toNumber, string fromNumber, Vonage.Voice.Nccos.Ncco ncco)
        {
            var command = new Voice.CallCommand
            {
                To = new[]
                {
                    new Voice.Nccos.Endpoints.PhoneEndpoint
                    {
                        Number=toNumber
                    }
                },
                From = new Voice.Nccos.Endpoints.PhoneEndpoint
                {
                    Number = fromNumber
                },
                Ncco = ncco
            };

            return ApiRequest.DoRequestWithJsonContentAsync<CallResponse>(
               POST,
               ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CALLS_ENDPOINT),
               command,
               ApiRequest.AuthType.Bearer,
               Credentials
               );
        }

        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="toEndPoint"></param>
        /// <param name="fromNumber"></param>
        /// <param name="ncco"></param>
        /// <returns></returns>
        public Task<CallResponse> CreateCallAsync(Endpoint toEndPoint, string fromNumber, Vonage.Voice.Nccos.Ncco ncco)
        {
            var command = new Voice.CallCommand
            {
                To = new[]
                {
                   toEndPoint
                },
                From = new Voice.Nccos.Endpoints.PhoneEndpoint
                {
                    Number = fromNumber
                },
                Ncco = ncco
            };

            return ApiRequest.DoRequestWithJsonContentAsync<CallResponse>(
               POST,
               ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CALLS_ENDPOINT),
               command,
               ApiRequest.AuthType.Bearer,
               Credentials
               );
        }

        public CallResponse CreateCall(string toNumber, string fromNumber, Ncco ncco)
        {
           return CreateCallAsync(toNumber,fromNumber,ncco).GetAwaiter().GetResult();
        }

        public CallResponse CreateCall(Endpoint toEndPoint, string fromNumber, Ncco ncco)
        {
           return CreateCallAsync(toEndPoint,fromNumber,ncco).GetAwaiter().GetResult();
        }
    }
}