#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.LiveCaptions.Start;

/// <inheritdoc />
[Builder]
public readonly partial struct StartRequest : IVonageRequest
{
    private const string DefaultLanguage = "en-US";
    private const int DefaultMaxDuration = 14400;
    private const int MinimalMaxDuration = 300;

    /// <summary>
    ///     A valid Vonage Video token with role set to Moderator.
    /// </summary>
    [JsonPropertyOrder(1)]
    [Mandatory(2)]
    public string Token { get; internal init; }

    /// <summary>
    ///     Vonage Application UUID
    /// </summary>
    [JsonIgnore]
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The session ID of the Vonage Video session. The audio from Publishers publishing into this session will be used to
    ///     generate the captions.
    /// </summary>
    [JsonPropertyOrder(0)]
    [Mandatory(1)]
    public string SessionId { get; internal init; }

    /// <summary>
    ///     Whether to enable this to faster captioning at the cost of some degree of inaccuracies.
    /// </summary>
    [JsonPropertyOrder(4)]
    [OptionalBoolean(true, "DisablePartialCaptions")]
    public bool PartialCaptions { get; internal init; }

    /// <summary>
    ///     The BCP-47 code for a spoken language used on this call. The default value is "en-US".
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonPropertyName("languageCode")]
    [OptionalWithDefault("string", DefaultLanguage)]
    public string Language { get; internal init; }

    /// <summary>
    ///     The maximum duration for the audio captioning, in seconds. The default value is 14,400 seconds (4 hours), the
    ///     maximum duration allowed. The minimum value for maxDuration is 300 (300 seconds, or 5 minutes).
    /// </summary>
    [JsonPropertyOrder(3)]
    [OptionalWithDefault("int", "14400")]
    public int MaxDuration { get; internal init; }

    /// <summary>
    ///     A publicly reachable URL controlled by the customer and capable of generating the content to be rendered without
    ///     user intervention
    /// </summary>
    [JsonPropertyOrder(5)]
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<Uri> StatusCallbackUrl { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/captions";

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<StartRequest> VerifyApplicationId(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<StartRequest> VerifySessionId(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    [ValidationRule]
    internal static Result<StartRequest> VerifyToken(StartRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Token, nameof(request.Token));

    [ValidationRule]
    internal static Result<StartRequest> VerifyMinimumDuration(StartRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.MaxDuration, MinimalMaxDuration,
            nameof(request.MaxDuration));

    [ValidationRule]
    internal static Result<StartRequest> VerifyMaximumDuration(StartRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.MaxDuration, DefaultMaxDuration,
            nameof(request.MaxDuration));
}