#region
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Exceptions;
using Vonage.Request;
#endregion

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
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, CallsEndpoint),
                command,
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<CallRecord> GetCallAsync(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<CallRecord>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"{CallsEndpoint}/{id}"),
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<PageResponse<CallList>> GetCallsAsync(CallSearchFilter filter, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<PageResponse<CallList>>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, CallsEndpoint),
                AuthType.Bearer,
                filter
            );

    /// <inheritdoc />
    public async Task<GetRecordingResponse> GetRecordingAsync(string recordingUrl, Credentials creds = null)
    {
        var validHosts = new[] {"nexmo.com", "vonage.com"};
        if (!Uri.TryCreate(recordingUrl, UriKind.Absolute, out var uri)
            || !validHosts.Any(host => uri.Host.EndsWith(host)))
        {
            throw new VonageException("Invalid uri");
        }

        using var response =
            await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
                .DoGetRequestWithJwtAsync(new Uri(recordingUrl)).ConfigureAwait(false);
        return new GetRecordingResponse
        {
            ResultStream = await ReadContent(response.Content).ConfigureAwait(false),
            Status = response.StatusCode,
        };
    }

    /// <inheritdoc />
    public Task<CallCommandResponse> StartDtmfAsync(string id, DtmfCommand cmd, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallCommandResponse>(
                HttpMethod.Put,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"{CallsEndpoint}/{id}/dtmf"),
                cmd,
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<CallCommandResponse> StartStreamAsync(string id, StreamCommand command, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallCommandResponse>(
                HttpMethod.Put,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"{CallsEndpoint}/{id}/stream"),
                command,
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<CallCommandResponse> StartTalkAsync(string id, TalkCommand cmd, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallCommandResponse>(
                HttpMethod.Put,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"{CallsEndpoint}/{id}/talk"),
                cmd,
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<CallCommandResponse> StopStreamAsync(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallCommandResponse>(
                HttpMethod.Delete,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"{CallsEndpoint}/{id}/stream"),
                new { },
                AuthType.Bearer
            );

    /// <inheritdoc />
    public Task<CallCommandResponse> StopTalkAsync(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallCommandResponse>(
                HttpMethod.Delete,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"{CallsEndpoint}/{id}/talk"),
                new { },
                AuthType.Bearer
            );

    /// <inheritdoc />
    public async Task<bool> UpdateCallAsync(string id, CallEditCommand command, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<CallRecord>(
                HttpMethod.Put,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"{CallsEndpoint}/{id}"),
                command,
                AuthType.Bearer
            ).ConfigureAwait(false);
        return true;
    }

    /// <inheritdoc />
    public async Task SubscribeRealTimeDtmf(string uuid, Uri eventUrl, Credentials creds = null) =>
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync(
                HttpMethod.Put,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"{CallsEndpoint}/{uuid}/input/dtmf"),
                new SubscribeRealTimeDtmfCommand([eventUrl]),
                AuthType.Bearer
            ).ConfigureAwait(false);

    /// <inheritdoc />
    public async Task UnsubscribeRealTimeDtmf(string uuid, Credentials creds = null) =>
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoDeleteRequestWithUrlContentAsync(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"{CallsEndpoint}/{uuid}/input/dtmf"),
                new Dictionary<string, string>(),
                AuthType.Bearer
            ).ConfigureAwait(false);

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.credentials;

    private static async Task<byte[]> ReadContent(HttpContent content)
    {
        var readTask = await content.ReadAsStreamAsync().ConfigureAwait(false);
        using var ms = new MemoryStream();
        await readTask.CopyToAsync(ms).ConfigureAwait(false);
        return ms.ToArray();
    }
}