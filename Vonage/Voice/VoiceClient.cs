using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;

namespace Vonage.Voice;

/// <inheritdoc />
public class VoiceClient : IVoiceClient
{
    private const string CallsEndpoint = "v1/calls";
    private readonly Credentials credentials;

    /// <summary>
    ///     Initializes a VoiceClient.
    /// </summary>
    /// <param name="credentials">Credentials to use for api calls.</param>
    public VoiceClient(Credentials credentials = null) => this.credentials = credentials;

    /// <inheritdoc />
    public CallResponse CreateCall(CallCommand command, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoRequestWithJsonContent<CallResponse>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            command,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public CallResponse CreateCall(string toNumber, string fromNumber, Ncco ncco)
    {
        var command = new CallCommand
        {
            To = new Endpoint[]
            {
                new PhoneEndpoint
                {
                    Number = toNumber,
                },
            },
            From = new PhoneEndpoint
            {
                Number = fromNumber,
            },
            Ncco = ncco,
        };
        return new ApiRequest(this.credentials).DoRequestWithJsonContent<CallResponse>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            command,
            AuthType.Bearer
        );
    }

    /// <inheritdoc />
    public CallResponse CreateCall(Endpoint toEndPoint, string fromNumber, Ncco ncco)
    {
        var command = new CallCommand
        {
            To = new[]
            {
                toEndPoint,
            },
            From = new PhoneEndpoint
            {
                Number = fromNumber,
            },
            Ncco = ncco,
        };
        return new ApiRequest(this.credentials).DoRequestWithJsonContent<CallResponse>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            command,
            AuthType.Bearer
        );
    }

    /// <inheritdoc />
    public Task<CallResponse> CreateCallAsync(CallCommand command, Credentials creds = null) =>
        new ApiRequest(this.credentials).DoRequestWithJsonContentAsync<CallResponse>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            command,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallResponse> CreateCallAsync(string toNumber, string fromNumber, Ncco ncco)
    {
        var command = new CallCommand
        {
            To = new Endpoint[]
            {
                new PhoneEndpoint
                {
                    Number = toNumber,
                },
            },
            From = new PhoneEndpoint
            {
                Number = fromNumber,
            },
            Ncco = ncco,
        };
        return new ApiRequest(this.credentials).DoRequestWithJsonContentAsync<CallResponse>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            command,
            AuthType.Bearer
        );
    }

    /// <inheritdoc />
    public Task<CallResponse> CreateCallAsync(Endpoint toEndPoint, string fromNumber, Ncco ncco)
    {
        var command = new CallCommand
        {
            To = new[]
            {
                toEndPoint,
            },
            From = new PhoneEndpoint
            {
                Number = fromNumber,
            },
            Ncco = ncco,
        };
        return new ApiRequest(this.credentials).DoRequestWithJsonContentAsync<CallResponse>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            command,
            AuthType.Bearer
        );
    }

    /// <inheritdoc />
    public CallRecord GetCall(string id, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoGetRequestWithQueryParameters<CallRecord>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}"),
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallRecord> GetCallAsync(string id, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoGetRequestWithQueryParametersAsync<CallRecord>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}"),
            AuthType.Bearer
        );

    /// <inheritdoc />
    public PageResponse<CallList> GetCalls(CallSearchFilter filter, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoGetRequestWithQueryParameters<PageResponse<CallList>>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            AuthType.Bearer,
            filter
        );

    /// <inheritdoc />
    public Task<PageResponse<CallList>> GetCallsAsync(CallSearchFilter filter, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoGetRequestWithQueryParametersAsync<PageResponse<CallList>>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            AuthType.Bearer,
            filter
        );

    /// <inheritdoc />
    public GetRecordingResponse GetRecording(string recordingUrl, Credentials creds = null)
    {
        using var response = new ApiRequest(this.GetCredentials(creds)).DoGetRequestWithJwt(new Uri(recordingUrl));
        var readTask = response.Content.ReadAsStreamAsync();
        byte[] bytes;
        readTask.Wait();
        using (var ms = new MemoryStream())
        {
            readTask.Result.CopyTo(ms);
            bytes = ms.ToArray();
        }

        return new GetRecordingResponse
        {
            ResultStream = bytes,
            Status = response.StatusCode,
        };
    }

    /// <inheritdoc />
    public async Task<GetRecordingResponse> GetRecordingAsync(string recordingUrl, Credentials creds = null)
    {
        using var response =
            await new ApiRequest(this.GetCredentials(creds)).DoGetRequestWithJwtAsync(new Uri(recordingUrl));
        var readTask = response.Content.ReadAsStreamAsync();
        byte[] bytes;
        readTask.Wait();
        using (var ms = new MemoryStream())
        {
            await readTask.Result.CopyToAsync(ms);
            bytes = ms.ToArray();
        }

        return new GetRecordingResponse
        {
            ResultStream = bytes,
            Status = response.StatusCode,
        };
    }

    /// <inheritdoc />
    public CallCommandResponse StartDtmf(string id, DtmfCommand cmd, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoRequestWithJsonContent<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/dtmf"),
            cmd,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallCommandResponse> StartDtmfAsync(string id, DtmfCommand cmd, Credentials creds = null) =>
        new ApiRequest(this.credentials).DoRequestWithJsonContentAsync<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/dtmf"),
            cmd,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public CallCommandResponse StartStream(string id, StreamCommand command, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoRequestWithJsonContent<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/stream"),
            command,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallCommandResponse> StartStreamAsync(string id, StreamCommand command, Credentials creds = null) =>
        new ApiRequest(this.credentials).DoRequestWithJsonContentAsync<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/stream"),
            command,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public CallCommandResponse StartTalk(string id, TalkCommand cmd, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoRequestWithJsonContent<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/talk"),
            cmd,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallCommandResponse> StartTalkAsync(string id, TalkCommand cmd, Credentials creds = null) =>
        new ApiRequest(this.credentials).DoRequestWithJsonContentAsync<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/talk"),
            cmd,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public CallCommandResponse StopStream(string id, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoRequestWithJsonContent<CallCommandResponse>(
            HttpMethod.Delete,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/stream"),
            new { },
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallCommandResponse> StopStreamAsync(string id, Credentials creds = null) =>
        new ApiRequest(this.credentials).DoRequestWithJsonContentAsync<CallCommandResponse>(
            HttpMethod.Delete,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/stream"),
            new { },
            AuthType.Bearer
        );

    /// <inheritdoc />
    public CallCommandResponse StopTalk(string id, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoRequestWithJsonContent<CallCommandResponse>(
            HttpMethod.Delete,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/talk"),
            new { },
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallCommandResponse> StopTalkAsync(string id, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds)).DoRequestWithJsonContentAsync<CallCommandResponse>(
            HttpMethod.Delete,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/talk"),
            new { },
            AuthType.Bearer
        );

    /// <inheritdoc />
    public bool UpdateCall(string id, CallEditCommand command, Credentials creds = null)
    {
        new ApiRequest(this.GetCredentials(creds)).DoRequestWithJsonContent<CallRecord>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}"),
            command,
            AuthType.Bearer
        );
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateCallAsync(string id, CallEditCommand command, Credentials creds = null)
    {
        await new ApiRequest(this.GetCredentials(creds)).DoRequestWithJsonContentAsync<CallRecord>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}"),
            command,
            AuthType.Bearer
        );
        return true;
    }

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.credentials;
}