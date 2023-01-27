using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Moderation.MuteStreams;

/// <summary>
///     Represents a request to mute streams.
/// </summary>
public readonly struct MuteStreamsRequest : IVonageRequest
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
    ///     The request content.
    /// </summary>
    public MuteStreamsConfiguration Configuration { get; }

    /// <summary>
    ///     The Video session Id.
    /// </summary>
    public string SessionId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/mute";

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

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this.Configuration),
            Encoding.UTF8,
            "application/json");

    private static Result<MuteStreamsRequest> VerifyApplicationId(MuteStreamsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<MuteStreamsRequest> VerifyExcludedStreams(MuteStreamsRequest request) =>
        InputValidation.VerifyNotNull(request, request.Configuration.ExcludedStreamIds,
            nameof(MuteStreamsConfiguration.ExcludedStreamIds));

    private static Result<MuteStreamsRequest> VerifySessionId(MuteStreamsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));

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