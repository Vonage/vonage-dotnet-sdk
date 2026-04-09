#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Common.Validation;
using Vonage.Serialization;
using Vonage.Server;
#endregion

namespace Vonage.Video.Archives.CreateArchive;

/// <summary>
///     Represents a request to creating an archive.
/// </summary>
[Builder("Vonage.Server")]
public readonly partial struct CreateArchiveRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    private const int MinimumQuantizationParameter = 15;
    private const int MaximumQuantizationParameter = 40;
    private const int MaximumMaxBitrate = 6000000;
    private const int MinimumMaxBitrate = 1000000;

    /// <summary>
    ///     Disables audio recording for the archive. By default, audio is recorded. If you disable both audio and video,
    ///     the request results in an error.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .DisableAudio()
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(1)]
    [OptionalBoolean(true, "DisableAudio")]
    public bool HasAudio { get; internal init; }

    /// <summary>
    ///     Enables transcription of the audio of the session. Disabled by default.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .EnableTranscription()
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(11)]
    [OptionalBoolean(false, "EnableTranscription")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool HasTranscription { get; internal init; }

    /// <summary>
    ///     Sets the transcription configuration.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithTranscription(new TranscriptionProperties { PrimaryLanguageCode = "en-US", HasSummary = true })
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(12)]
    [JsonPropertyName("transcriptionProperties")]
    [Optional]
    [JsonConverter(typeof(MaybeJsonConverter<TranscriptionProperties>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<TranscriptionProperties> Transcription { get; internal init; }

    /// <summary>
    ///     Disables video recording for the archive. By default, video is recorded. If you disable both audio and video,
    ///     the request results in an error.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .DisableVideo()
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(2)]
    [OptionalBoolean(true, "DisableVideo")]
    public bool HasVideo { get; internal init; }

    /// <summary>
    ///     Sets the archive's layout.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithLayout(new Layout(null, null, LayoutType.BestFit))
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(3)]
    [JsonConverter(typeof(MaybeJsonConverter<Layout>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<Layout> Layout { get; internal init; }

    /// <summary>
    ///     Sets the name of the archive (for your own identification).
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithName("My Archive")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(4)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     Sets whether all streams in the archive are recorded to a single file ("composed", the default) or to individual
    ///     files ("individual").
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithOutputMode(OutputMode.Individual)
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(5)]
    [JsonConverter(typeof(MaybeJsonConverter<OutputMode>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [OptionalWithDefault("OutputMode", "OutputMode.Composed")]
    public Maybe<OutputMode> OutputMode { get; internal init; }

    /// <summary>
    ///     Sets the resolution of the archive. Only applies to composed archives. If you set this and set the outputMode to
    ///     "individual", the request results in an error. Available resolutions: "640x480" (SD landscape, the default),
    ///     "1280x720" (HD landscape), "1920x1080" (FHD landscape), "480x640" (SD portrait), "720x1280" (HD portrait), or
    ///     "1080x1920" (FHD portrait).
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithResolution(RenderResolution.HighDefinitionLandscape)
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(6)]
    [JsonConverter(typeof(MaybeJsonConverter<RenderResolution>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<RenderResolution> Resolution { get; internal init; }

    /// <summary>
    ///     Sets whether streams included in the archive are selected automatically ("auto", the default) or manually
    ///     ("manual"). In manual mode, you specify streams to be included and whether a stream's audio, video, or both are
    ///     included. In composed archives, the archive composer includes streams based on stream prioritization rules.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithStreamMode(StreamMode.Manual)
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(7)]
    [JsonConverter(typeof(MaybeJsonConverter<StreamMode>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [OptionalWithDefault("StreamMode", "StreamMode.Auto")]
    public Maybe<StreamMode> StreamMode { get; internal init; }

    /// <summary>
    ///     Sets a unique tag to support recording multiple archives for the same session simultaneously. You must also set
    ///     this option when manually starting an archive in a session that is automatically archived. If you do not specify
    ///     a unique multiArchiveTag, you can only record one archive at a time for a given session.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithMultiArchiveTag("my-multi-archive")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(8)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<string> MultiArchiveTag { get; internal init; }

    /// <summary>
    ///     Sets the maximum video bitrate for the archive, in bits per second. Only valid for composed archives.
    ///     Valid range: 1,000,000 to 6,000,000. This maximum applies to the video bitrate only; audio bits are excluded
    ///     from the limit.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithMaxBitrate(2000000)
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(9)]
    [JsonConverter(typeof(MaybeJsonConverter<int>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<int> MaxBitrate { get; internal init; }

    /// <summary>
    ///     Sets the quantization parameter (QP) for composed archives. Smaller values (min 15) generate higher quality and
    ///     larger archives; larger values (max 40) generate lower quality and smaller archives. Uses variable bitrate (VBR).
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithQuantizationParameter(25)
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(10)]
    [JsonConverter(typeof(MaybeJsonConverter<int>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<int> QuantizationParameter { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    [Mandatory(1)]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, $"/v2/project/{this.ApplicationId}/archive")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<CreateArchiveRequest> VerifyApplicationId(CreateArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<CreateArchiveRequest> VerifySessionId(CreateArchiveRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    [ValidationRule]
    internal static Result<CreateArchiveRequest> VerifyMinimumMaxBitrate(CreateArchiveRequest request) =>
        request.MaxBitrate.Match(
            some => InputValidation.VerifyHigherOrEqualThan(request, some, MinimumMaxBitrate,
                nameof(request.MaxBitrate)),
            () => request);

    [ValidationRule]
    internal static Result<CreateArchiveRequest> VerifyMaximumMaxBitrate(CreateArchiveRequest request) =>
        request.MaxBitrate.Match(
            some => InputValidation.VerifyLowerOrEqualThan(request, some, MaximumMaxBitrate,
                nameof(request.MaxBitrate)),
            () => request);

    [ValidationRule]
    internal static Result<CreateArchiveRequest> VerifyMinimumQuantizationParameter(CreateArchiveRequest request) =>
        request.QuantizationParameter.Match(
            some => InputValidation.VerifyHigherOrEqualThan(request, some, MinimumQuantizationParameter,
                nameof(request.QuantizationParameter)),
            () => request);

    [ValidationRule]
    internal static Result<CreateArchiveRequest> VerifyMaximumQuantizationParameter(CreateArchiveRequest request) =>
        request.QuantizationParameter.Match(
            some => InputValidation.VerifyLowerOrEqualThan(request, some, MaximumQuantizationParameter,
                nameof(request.QuantizationParameter)),
            () => request);
}