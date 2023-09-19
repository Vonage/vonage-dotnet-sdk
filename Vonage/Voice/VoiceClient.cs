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
    private readonly Configuration configuration;

    /// <summary>
    ///     Initializes a VoiceClient.
    /// </summary>
    /// <param name="credentials">Credentials to use for api calls.</param>
    public VoiceClient(Credentials credentials = null)
    {
        this.credentials = credentials;
        this.configuration = Configuration.Instance;
    }

    internal VoiceClient(Credentials credentials, Configuration configuration)
    {
        this.credentials = credentials;
        this.configuration = configuration;
    }

    /// <inheritdoc />
    public CallResponse CreateCall(CallCommand command, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContent<CallResponse>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            command,
            AuthType.Bearer
        );

    /// <inheritdoc />
    [Obsolete("Favor 'CreateCall(CallCommand) instead.'")]
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
        return this.CreateCall(command);
    }

    /// <inheritdoc />
    [Obsolete("Favor 'CreateCall(CallCommand) instead.'")]
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
        return this.CreateCall(command);
    }

    /// <inheritdoc />
    public Task<CallResponse> CreateCallAsync(CallCommand command, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContentAsync<CallResponse>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            command,
            AuthType.Bearer
        );

    /// <inheritdoc />
    [Obsolete("Favor 'CreateCallAsync(CallCommand) instead.'")]
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
        return this.CreateCallAsync(command);
    }

    /// <inheritdoc />
    [Obsolete("Favor 'CreateCallAsync(CallCommand) instead.'")]
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
        return this.CreateCallAsync(command);
    }

    /// <inheritdoc />
    public CallRecord GetCall(string id, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParameters<CallRecord>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}"),
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallRecord> GetCallAsync(string id, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParametersAsync<CallRecord>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}"),
            AuthType.Bearer
        );

    /// <inheritdoc />
    public PageResponse<CallList> GetCalls(CallSearchFilter filter, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParameters<PageResponse<CallList>>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            AuthType.Bearer,
            filter
        );

    /// <inheritdoc />
    public Task<PageResponse<CallList>> GetCallsAsync(CallSearchFilter filter, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParametersAsync<PageResponse<CallList>>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
            AuthType.Bearer,
            filter
        );

    /// <inheritdoc />
    public GetRecordingResponse GetRecording(string recordingUrl, Credentials creds = null)
    {
        using var response = new ApiRequest(this.GetCredentials(creds), this.configuration).DoGetRequestWithJwt(new Uri(recordingUrl));
        return new GetRecordingResponse
        {
            ResultStream = ReadContent(response.Content).Result,
            Status = response.StatusCode,
        };
    }

    /// <inheritdoc />
    public async Task<GetRecordingResponse> GetRecordingAsync(string recordingUrl, Credentials creds = null)
    {
        using var response =
            await new ApiRequest(this.GetCredentials(creds), this.configuration).DoGetRequestWithJwtAsync(new Uri(recordingUrl));
        return new GetRecordingResponse
        {
            ResultStream = await ReadContent(response.Content),
            Status = response.StatusCode,
        };
    }

    /// <inheritdoc />
    public CallCommandResponse StartDtmf(string id, DtmfCommand cmd, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContent<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/dtmf"),
            cmd,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallCommandResponse> StartDtmfAsync(string id, DtmfCommand cmd, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContentAsync<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/dtmf"),
            cmd,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public CallCommandResponse StartStream(string id, StreamCommand command, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContent<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/stream"),
            command,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallCommandResponse> StartStreamAsync(string id, StreamCommand command, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContentAsync<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/stream"),
            command,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public CallCommandResponse StartTalk(string id, TalkCommand cmd, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContent<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/talk"),
            cmd,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallCommandResponse> StartTalkAsync(string id, TalkCommand cmd, Credentials creds = null) =>
        new ApiRequest(this.credentials, this.configuration).DoRequestWithJsonContentAsync<CallCommandResponse>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/talk"),
            cmd,
            AuthType.Bearer
        );

    /// <inheritdoc />
    public CallCommandResponse StopStream(string id, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContent<CallCommandResponse>(
            HttpMethod.Delete,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/stream"),
            new { },
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallCommandResponse> StopStreamAsync(string id, Credentials creds = null) =>
        new ApiRequest(this.credentials, this.configuration).DoRequestWithJsonContentAsync<CallCommandResponse>(
            HttpMethod.Delete,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/stream"),
            new { },
            AuthType.Bearer
        );

    /// <inheritdoc />
    public CallCommandResponse StopTalk(string id, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContent<CallCommandResponse>(
            HttpMethod.Delete,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/talk"),
            new { },
            AuthType.Bearer
        );

    /// <inheritdoc />
    public Task<CallCommandResponse> StopTalkAsync(string id, Credentials creds = null) =>
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContentAsync<CallCommandResponse>(
            HttpMethod.Delete,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/talk"),
            new { },
            AuthType.Bearer
        );

    /// <inheritdoc />
    public bool UpdateCall(string id, CallEditCommand command, Credentials creds = null)
    {
        new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContent<CallRecord>(
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
        await new ApiRequest(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContentAsync<CallRecord>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}"),
            command,
            AuthType.Bearer
        );
        return true;
    }

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.credentials;

    private static async Task<byte[]> ReadContent(HttpContent content)
    {
        var readTask = await content.ReadAsStreamAsync();
        using var ms = new MemoryStream();
        await readTask.CopyToAsync(ms);
        return ms.ToArray();
    }
}