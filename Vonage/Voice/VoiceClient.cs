using System;
using System.IO;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Exceptions;
using Vonage.Request;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;

namespace Vonage.Voice;

public class VoiceClient : IVoiceClient
{
    public const string CALLS_ENDPOINT = "v1/calls";
    public const string DELETE = "DELETE";
    public const string POST = "POST";
    public const string PUT = "PUT";
    public Credentials Credentials { get; set; }

    public VoiceClient(Credentials credentials = null)
    {
        this.Credentials = credentials;
    }

    public CallResponse CreateCall(CallCommand command, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContent<CallResponse>(
            POST,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CALLS_ENDPOINT),
            command,
            ApiRequest.AuthType.Bearer,
            creds ?? this.Credentials
        );
    }

    public CallResponse CreateCall(string toNumber, string fromNumber, Ncco ncco)
    {
        var command = new CallCommand
        {
            To = new[]
            {
                new PhoneEndpoint
                {
                    Number = toNumber
                }
            },
            From = new PhoneEndpoint
            {
                Number = fromNumber
            },
            Ncco = ncco
        };
        return ApiRequest.DoRequestWithJsonContent<CallResponse>(
            POST,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CALLS_ENDPOINT),
            command,
            ApiRequest.AuthType.Bearer, this.Credentials
        );
    }

    public CallResponse CreateCall(Endpoint toEndPoint, string fromNumber, Ncco ncco)
    {
        var command = new CallCommand
        {
            To = new[]
            {
                toEndPoint
            },
            From = new PhoneEndpoint
            {
                Number = fromNumber
            },
            Ncco = ncco
        };
        return ApiRequest.DoRequestWithJsonContent<CallResponse>(
            POST,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CALLS_ENDPOINT),
            command,
            ApiRequest.AuthType.Bearer, this.Credentials
        );
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
            creds ?? this.Credentials
        );
    }

    /// <summary>
    /// POST /v1/calls - create an outbound SIP or PSTN Call
    /// </summary>
    /// <param name="toNumber"></param>
    /// <param name="fromNumber"></param>
    /// <param name="ncco"></param>
    /// <returns></returns>
    public Task<CallResponse> CreateCallAsync(string toNumber, string fromNumber, Ncco ncco)
    {
        var command = new CallCommand
        {
            To = new[]
            {
                new PhoneEndpoint
                {
                    Number = toNumber
                }
            },
            From = new PhoneEndpoint
            {
                Number = fromNumber
            },
            Ncco = ncco
        };
        return ApiRequest.DoRequestWithJsonContentAsync<CallResponse>(
            POST,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CALLS_ENDPOINT),
            command,
            ApiRequest.AuthType.Bearer, this.Credentials
        );
    }

    /// <summary>
    /// POST /v1/calls - create an outbound SIP or PSTN Call
    /// </summary>
    /// <param name="toEndPoint"></param>
    /// <param name="fromNumber"></param>
    /// <param name="ncco"></param>
    /// <returns></returns>
    public Task<CallResponse> CreateCallAsync(Endpoint toEndPoint, string fromNumber, Ncco ncco)
    {
        var command = new CallCommand
        {
            To = new[]
            {
                toEndPoint
            },
            From = new PhoneEndpoint
            {
                Number = fromNumber
            },
            Ncco = ncco
        };
        return ApiRequest.DoRequestWithJsonContentAsync<CallResponse>(
            POST,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CALLS_ENDPOINT),
            command,
            ApiRequest.AuthType.Bearer, this.Credentials
        );
    }

    public CallRecord GetCall(string id, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<CallRecord>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}"),
            ApiRequest.AuthType.Bearer,
            credentials: creds ?? this.Credentials
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
            credentials: creds ?? this.Credentials
        );
    }

    public PageResponse<CallList> GetCalls(CallSearchFilter filter, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<PageResponse<CallList>>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CALLS_ENDPOINT),
            ApiRequest.AuthType.Bearer,
            filter,
            creds ?? this.Credentials
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
            creds ?? this.Credentials
        );
    }

    public GetRecordingResponse GetRecording(string recordingUrl, Credentials creds = null)
    {
        using (var response = ApiRequest.DoGetRequestWithJwt(new Uri(recordingUrl), creds ?? this.Credentials))
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

    /// <summary>
    /// GET - retrieves the recording from a call based off of the input url
    /// </summary>
    /// <param name="recordingUrl">Url where the recorded call lives</param>
    /// <param name="creds">Overridden credentials</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    /// <returns>A response containing a byte array representing the file stream</returns>
    public async Task<GetRecordingResponse> GetRecordingAsync(string recordingUrl, Credentials creds = null)
    {
        using (var response =
               await ApiRequest.DoGetRequestWithJwtAsync(new Uri(recordingUrl), creds ?? this.Credentials))
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

    public CallCommandResponse StartDtmf(string id, DtmfCommand cmd, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>(
            PUT,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}/dtmf"),
            cmd,
            ApiRequest.AuthType.Bearer,
            creds ?? this.Credentials
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
            creds ?? this.Credentials
        );
    }

    public CallCommandResponse StartStream(string id, StreamCommand command, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>(
            PUT,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}/stream"),
            command,
            ApiRequest.AuthType.Bearer,
            creds ?? this.Credentials
        );
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
            creds ?? this.Credentials
        );
    }

    public CallCommandResponse StartTalk(string id, TalkCommand cmd, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>(
            PUT,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}/talk"),
            cmd,
            ApiRequest.AuthType.Bearer,
            creds ?? this.Credentials
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
            creds ?? this.Credentials
        );
    }

    public CallCommandResponse StopStream(string id, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>(
            DELETE,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}/stream"),
            new { },
            ApiRequest.AuthType.Bearer,
            creds ?? this.Credentials
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
            creds ?? this.Credentials
        );
    }

    public CallCommandResponse StopTalk(string id, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContent<CallCommandResponse>(
            DELETE,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}/talk"),
            new { },
            ApiRequest.AuthType.Bearer,
            creds ?? this.Credentials
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
            creds ?? this.Credentials
        );
    }

    public bool UpdateCall(string id, CallEditCommand command, Credentials creds = null)
    {
        ApiRequest.DoRequestWithJsonContent<CallRecord>(
            PUT,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CALLS_ENDPOINT}/{id}"),
            command,
            ApiRequest.AuthType.Bearer,
            creds ?? this.Credentials
        );
        return true;
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
            creds ?? this.Credentials
        );
        return true;
    }
}