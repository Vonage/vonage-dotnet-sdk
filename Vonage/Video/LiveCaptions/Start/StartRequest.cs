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
    ///     Sets the Vonage Video token with role set to Moderator.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithToken("eyJ0eXAiOiJKV1Q...")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(1)]
    [Mandatory(2)]
    public string Token { get; internal init; }

    /// <summary>
    ///     Sets the Vonage Application UUID.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithApplicationId(applicationId)
    /// ]]></code>
    /// </example>
    [JsonIgnore]
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     Sets the Vonage Video session ID. The audio from Publishers publishing into this session will be used to generate
    ///     the captions.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(0)]
    [Mandatory(1)]
    public string SessionId { get; internal init; }

    /// <summary>
    ///     Disables partial captions. Partial captions are enabled by default for faster captioning at the cost of some
    ///     degree of inaccuracies.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .DisablePartialCaptions()
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(4)]
    [OptionalBoolean(true, "DisablePartialCaptions")]
    public bool PartialCaptions { get; internal init; }

    /// <summary>
    ///     Sets the BCP-47 code for a spoken language used on this call. The default value is "en-US".
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithLanguage("es-ES")
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(2)]
    [JsonPropertyName("languageCode")]
    [OptionalWithDefault("string", DefaultLanguage)]
    public string Language { get; internal init; }

    /// <summary>
    ///     Sets the maximum duration for the audio captioning, in seconds. The default value is 14,400 seconds (4 hours),
    ///     the maximum duration allowed. The minimum value is 300 (5 minutes).
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithMaxDuration(3600)
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(3)]
    [OptionalWithDefault("int", "14400")]
    public int MaxDuration { get; internal init; }

    /// <summary>
    ///     Sets the callback URL for captioning status events.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithStatusCallbackUrl(new Uri("https://example.com/captions/status"))
    /// ]]></code>
    /// </example>
    [JsonPropertyOrder(5)]
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Optional]
    public Maybe<Uri> StatusCallbackUrl { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/v2/project/{this.ApplicationId}/captions")
        .WithContent(this.GetRequestContent())
        .Build();

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