using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Serialization;

namespace Vonage.Video.Moderation.MuteStreams;

/// <summary>
///     Represents a request to mute streams.
/// </summary>
public readonly struct MuteStreamsRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The request content.
    /// </summary>
    public MuteStreamsConfiguration Configuration { get; internal init; }

    /// <inheritdoc />
    public string SessionId { get; internal init; }

    /// <summary>
    /// Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new MuteStreamsRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/mute";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build(JsonNamingPolicy.CamelCase).SerializeObject(this.Configuration),
            Encoding.UTF8,
            "application/json");

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