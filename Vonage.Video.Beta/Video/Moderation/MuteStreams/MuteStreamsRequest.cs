using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Video.Moderation.MuteStreams;

/// <summary>
///     Represents a request to mute streams.
/// </summary>
public readonly struct MuteStreamsRequest : IVideoRequest
{
    private MuteStreamsRequest(string applicationId, string sessionId, MuteStreamsConfiguration configuration)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.Configuration = configuration;
    }

    /// <summary>
    ///     The Vonage application UUID.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     The Video session Id.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     The request content.
    /// </summary>
    public MuteStreamsConfiguration Configuration { get; }

    /// <summary>
    ///     Parses the input into a MuteStreamsRequest.
    /// </summary>
    /// <param name="applicationId">The Vonage application UUID.</param>
    /// <param name="sessionId">The Video session Id.</param>
    /// <param name="configuration"> The request configuration.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<MuteStreamsRequest> Parse(string applicationId, string sessionId,
        MuteStreamsConfiguration configuration) =>
        Result<MuteStreamsRequest>
            .FromSuccess(new MuteStreamsRequest(applicationId, sessionId, configuration))
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId)
            .Bind(VerifyExcludedStreams);

    /// <summary>
    ///     Retrieves the endpoint's path.
    /// </summary>
    /// <returns>The endpoint's path.</returns>
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/mute";

    /// <summary>
    ///     Creates a Http request for retrieving a stream.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>The Http request.</returns>
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        httpRequest.Content = new StringContent(new JsonSerializer().SerializeObject(this.Configuration), Encoding.UTF8,
            "application/json");
        return httpRequest;
    }

    private static Result<MuteStreamsRequest> VerifyApplicationId(MuteStreamsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<MuteStreamsRequest> VerifySessionId(MuteStreamsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));

    private static Result<MuteStreamsRequest> VerifyExcludedStreams(MuteStreamsRequest request) =>
        InputValidation.VerifyNotNull(request, request.Configuration.ExcludedStreamIds,
            nameof(MuteStreamsConfiguration.ExcludedStreamIds));

    /// <summary>
    ///     Represents a configuration for muting streams.
    /// </summary>
    public struct MuteStreamsConfiguration
    {
        /// <summary>
        ///     Whether to mute streams in the session (true) and enable the mute state of the session, or to disable the mute
        ///     state of the session (false). With the mute state enabled (true), all current and future streams published to the
        ///     session (with the exception of streams in the excludedStreamIds array) are muted. When you call this method with
        ///     the active property set to false, future streams published to the session are not muted (but any existing muted
        ///     streams remain muted).
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        ///     The stream IDs for streams that should not be muted. This is an optional property. If you omit this property, all
        ///     streams in the session will be muted. This property only applies when the active property is set to true. When the
        ///     active property is set to false, it is ignored. The elements in the excludedStreamIds array are stream IDs
        ///     (strings) for the streams you wish to exclude from being muted. If you do not wish to include an array of excluded
        ///     streams, do not include any body content.
        /// </summary>
        public string[] ExcludedStreamIds { get; set; }

        /// <summary>
        ///     Creates a configuration.
        /// </summary>
        /// <param name="active">
        ///     Whether to mute streams in the session (true) and enable the mute state of the session, or to
        ///     disable the mute state of the session (false). With the mute state enabled (true), all current and future streams
        ///     published to the session (with the exception of streams in the excludedStreamIds array) are muted. When you call
        ///     this method with the active property set to false, future streams published to the session are not muted (but any
        ///     existing muted streams remain muted).
        /// </param>
        /// <param name="excludedStreamIds">
        ///     The stream IDs for streams that should not be muted. This is an optional property. If
        ///     you omit this property, all streams in the session will be muted. This property only applies when the active
        ///     property is set to true. When the active property is set to false, it is ignored. The elements in the
        ///     excludedStreamIds array are stream IDs (strings) for the streams you wish to exclude from being muted. If you do
        ///     not wish to include an array of excluded streams, do not include any body content.
        /// </param>
        public MuteStreamsConfiguration(bool active, string[] excludedStreamIds)
        {
            this.Active = active;
            this.ExcludedStreamIds = excludedStreamIds;
        }
    }
}