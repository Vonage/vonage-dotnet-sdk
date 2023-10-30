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
    private readonly Configuration configuration;
    private readonly Credentials credentials;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    /// <summary>
    ///     Initializes a VoiceClient.
    /// </summary>
    /// <param name="credentials">Credentials to use for api calls.</param>
    public VoiceClient(Credentials credentials = null)
    {
        this.credentials = credentials;
        this.configuration = Configuration.Instance;
    }

    internal VoiceClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.credentials = credentials;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    /// <inheritdoc />
    public Task<CallResponse> CreateCallAsync(CallCommand command, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallResponse>(
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
    public Task<CallRecord> GetCallAsync(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<CallRecord>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}"),
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<PageResponse<CallList>> GetCallsAsync(CallSearchFilter filter, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<PageResponse<CallList>>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, CallsEndpoint),
                AuthType.Bearer,
                filter
            );

    /// <inheritdoc />
    public async Task<GetRecordingResponse> GetRecordingAsync(string recordingUrl, Credentials creds = null)
    {
        using var response =
            await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
                .DoGetRequestWithJwtAsync(new Uri(recordingUrl));
        return new GetRecordingResponse
        {
            ResultStream = await ReadContent(response.Content),
            Status = response.StatusCode,
        };
    }

    /// <inheritdoc />
    public Task<CallCommandResponse> StartDtmfAsync(string id, DtmfCommand cmd, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallCommandResponse>(
                HttpMethod.Put,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/dtmf"),
                cmd,
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<CallCommandResponse> StartStreamAsync(string id, StreamCommand command, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallCommandResponse>(
                HttpMethod.Put,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/stream"),
                command,
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<CallCommandResponse> StartTalkAsync(string id, TalkCommand cmd, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallCommandResponse>(
                HttpMethod.Put,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/talk"),
                cmd,
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<CallCommandResponse> StopStreamAsync(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallCommandResponse>(
                HttpMethod.Delete,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/stream"),
                new { },
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<CallCommandResponse> StopTalkAsync(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallCommandResponse>(
                HttpMethod.Delete,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"{CallsEndpoint}/{id}/talk"),
                new { },
                AuthType.Bearer
            );

    /// <inheritdoc />
    public async Task<bool> UpdateCallAsync(string id, CallEditCommand command, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallRecord>(
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