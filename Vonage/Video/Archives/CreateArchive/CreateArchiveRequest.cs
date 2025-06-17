#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;
using Vonage.Server;
#endregion

namespace Vonage.Video.Archives.CreateArchive;

/// <summary>
///     Represents a request to creating an archive.
/// </summary>
public readonly struct CreateArchiveRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    /// <summary>
    ///     Whether the archive will record audio (true, the default) or not (false). If you set both hasAudio and hasVideo to
    ///     false, the call to this method results in an error.
    /// </summary>
    [JsonPropertyOrder(1)]
    public bool HasAudio { get; internal init; }

    /// <summary>
    ///     Whether the archive will record video (true, the default) or not (false). If you set both hasAudio and hasVideo to
    ///     false, the call to this method results in an error.
    /// </summary>
    [JsonPropertyOrder(2)]
    public bool HasVideo { get; internal init; }

    /// <summary>
    ///     Represents the archive's layout.
    /// </summary>
    [JsonPropertyOrder(3)]
    public Layout Layout { get; internal init; }

    /// <summary>
    ///     The name of the archive (for your own identification).
    /// </summary>
    [JsonPropertyOrder(4)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     Whether all streams in the archive are recorded to a single file ("composed", the default) or to individual files
    ///     ("individual").
    /// </summary>
    [JsonPropertyOrder(5)]
    public OutputMode OutputMode { get; internal init; }

    /// <summary>
    ///     The resolution of the archive, either "640x480" (SD landscape, the default), "1280x720" (HD landscape), "1920x1080"
    ///     (FHD landscape), "480x640" (SD portrait), "720x1280" (HD portrait), or "1080x1920" (FHD portrait). You may want to
    ///     use a portrait aspect ratio for archives that include video streams from mobile devices (which often use the
    ///     portrait aspect ratio). This property only applies to composed archives. If you set this property and set the
    ///     outputMode property to "individual", the call to the REST method results in an error.
    /// </summary>
    [JsonPropertyOrder(6)]
    [JsonConverter(typeof(MaybeJsonConverter<RenderResolution>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<RenderResolution> Resolution { get; internal init; }

    /// <summary>
    ///     Whether streams included in the archive are selected automatically ("auto", the default) or manually ("manual").
    ///     When streams are selected automatically ("auto"), all streams in the session can be included in the archive. When
    ///     streams are selected manually ("manual"), you specify streams to be included based on calls to this REST method.
    ///     You can specify whether a stream's audio, video, or both are included in the archive. In composed archives, in both
    ///     automatic and manual modes, the archive composer includes streams based on stream prioritization rules.
    /// </summary>
    [JsonPropertyOrder(7)]
    public StreamMode StreamMode { get; internal init; }

    /// <summary>
    ///     Set this to support recording multiple archives for the same session simultaneously. Set this to a unique string
    ///     for each simultaneous archive of an ongoing session. You must also set this option when manually starting an
    ///     archive in a session that is automatically archived. If you do not specify a unique multiArchiveTag, you can only
    ///     record one archive at a time for a given session.
    /// </summary>
    [JsonPropertyOrder(8)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> MultiArchiveTag { get; internal init; }

    /// <summary>
    ///     The maximum video bitrate for the archive, in bits per second. This option is only valid for composed archives. Set
    ///     the maximum video bitrate to control the size of the composed archive. This maximum bitrate applies to the video
    ///     bitrate only. If the output archive has audio, those bits will be excluded from the limit.
    /// </summary>
    [JsonPropertyOrder(9)]
    [JsonConverter(typeof(MaybeJsonConverter<int>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<int> MaxBitrate { get; internal init; }

    /// <summary>
    ///     A video encoding value allowed for composed archiving, smaller values generate higher quality and larger archives,
    ///     larger values generate lower quality and smaller archives.
    ///     QuantizationParameter uses a variable bitrate.
    /// </summary>
    [JsonPropertyOrder(10)]
    [JsonConverter(typeof(MaybeJsonConverter<int>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<int> QuantizationParameter { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive";

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new CreateArchiveRequestBuilder();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
}