#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Common.Validation;
using Vonage.Serialization;
using Vonage.Server;
#endregion

namespace Vonage.Video.Broadcast.StartBroadcast;

/// <summary>
///     Represents a request to start a broadcast.
/// </summary>
[Builder("Vonage.Server")]
public readonly partial struct StartBroadcastRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    private const int MaximumMaxDuration = 36000;
    private const int MinimumMaxDuration = 60;

    /// <summary>
    ///     Specify this to assign the initial layout type for the broadcast. If you do not specify an initial layout type, the
    ///     broadcast stream uses the Best Fit layout type. For more information, see Configuring Video Layout for the OpenTok
    ///     live streaming feature.
    /// </summary>
    [JsonPropertyOrder(1)]
    [Mandatory(2)]
    public Layout Layout { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(3)]
    [OptionalWithDefault("int", "1000")]
    public int MaxBitrate { get; internal init; }

    /// <summary>
    ///     The maximum duration for the broadcast, in seconds. The broadcast will automatically stop when the maximum duration
    ///     is reached. You can set the maximum duration to a value from 60 (60 seconds) to 36000 (10 hours). The default
    ///     maximum duration is 4 hours (14,400 seconds)
    /// </summary>
    [JsonPropertyOrder(2)]
    [OptionalWithDefault("int", "14400")]
    public int MaxDuration { get; internal init; }

    /// <summary>
    ///     Set this to support multiple broadcasts for the same session simultaneously. Set this to a unique string for each
    ///     simultaneous broadcast of an ongoing session. See Simultaneous broadcasts.
    /// </summary>
    [JsonPropertyOrder(7)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<string> MultiBroadcastTag { get; internal init; }

    /// <summary>
    ///     This object defines the types of broadcast streams you want to start (both HLS and RTMP). You can include HLS,
    ///     RTMP, or both as broadcast streams. If you include RTMP streaming, you can specify up to five target RTMP streams
    ///     (or just one). Vonage streams the session to each RTMP URL you specify. Note that OpenTok live streaming supports
    ///     RTMP and RTMPS.
    /// </summary>
    [JsonPropertyOrder(4)]
    [Mandatory(3)]
    public BroadcastOutput Outputs { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(5)]
    [OptionalWithDefault("RenderResolution", "RenderResolution.StandardDefinitionLandscape")]
    public RenderResolution Resolution { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(6)]
    [OptionalWithDefault("StreamMode", "StreamMode.Auto")]
    public StreamMode StreamMode { get; internal init; }

    /// <summary>
    ///     Vonage Application UUID.
    /// </summary>
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
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast";

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<StartBroadcastRequest> VerifyApplicationId(StartBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<StartBroadcastRequest> VerifyHls(StartBroadcastRequest request) =>
        request.Outputs.Hls
            .Map(value => value.LowLatency && value.Dvr)
            .IfNone(false)
            ? Result<StartBroadcastRequest>.FromFailure(
                ResultFailure.FromErrorMessage("Dvr and LowLatency cannot be both set to true."))
            : request;

    [ValidationRule]
    internal static Result<StartBroadcastRequest> VerifyLayout(StartBroadcastRequest request) =>
        new
            {
                IsCustomType = request.Layout.Type == LayoutType.Custom,
                IsBestFitType = request.Layout.Type == LayoutType.BestFit,
                IsStylesheetEmpty = string.IsNullOrWhiteSpace(request.Layout.Stylesheet),
                IsScreenshareTypeSet = request.Layout.ScreenshareType != null,
            }
            switch
            {
                {IsScreenshareTypeSet: true, IsStylesheetEmpty: false} =>
                    ResultFailure.FromErrorMessage("Stylesheet should be null when screenshare type is set.")
                        .ToResult<StartBroadcastRequest>(),
                {IsScreenshareTypeSet: true, IsBestFitType: false} =>
                    ResultFailure.FromErrorMessage("Type should be BestFit when screenshare type is set.")
                        .ToResult<StartBroadcastRequest>(),
                {IsCustomType: true, IsStylesheetEmpty: true} =>
                    ResultFailure.FromErrorMessage("Stylesheet cannot be null or whitespace when type is Custom.")
                        .ToResult<StartBroadcastRequest>(),
                {IsCustomType: false, IsStylesheetEmpty: false} =>
                    ResultFailure.FromErrorMessage("Stylesheet should be null or whitespace when type is not Custom.")
                        .ToResult<StartBroadcastRequest>(),
                _ => request,
            };

    [ValidationRule]
    internal static Result<StartBroadcastRequest> VerifyMaxDuration(StartBroadcastRequest request) =>
        InputValidation
            .VerifyHigherOrEqualThan(request, request.MaxDuration, MinimumMaxDuration, nameof(request.MaxDuration))
            .Bind(_ => InputValidation.VerifyLowerOrEqualThan(request, request.MaxDuration, MaximumMaxDuration,
                nameof(request.MaxDuration)));

    [ValidationRule]
    internal static Result<StartBroadcastRequest> VerifySessionId(StartBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    /// <summary>
    ///     Defines the output for starting a broadcast.
    /// </summary>
    public struct BroadcastOutput
    {
        /// <summary>
        /// </summary>
        [JsonConverter(typeof(MaybeJsonConverter<Broadcast.HlsSettings>))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Maybe<Broadcast.HlsSettings> Hls { get; set; }

        /// <summary>
        ///     Represents the types of broadcast streams you want to start.
        /// </summary>
        [JsonPropertyName("rtmp")]
        public Stream[] Streams { get; set; }

        /// <summary>
        ///     Represents a stream to broadcast.
        /// </summary>
        /// <param name="Id">The unique ID for the stream.</param>
        /// <param name="ServerUrl">The RTMP server url.</param>
        /// <param name="StreamName">The stream name.</param>
        public record Stream(Guid Id, string ServerUrl, string StreamName);
    }
}