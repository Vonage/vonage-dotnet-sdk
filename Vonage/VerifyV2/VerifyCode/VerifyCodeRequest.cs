using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;

namespace Vonage.VerifyV2.VerifyCode;

/// <inheritdoc />
public readonly struct VerifyCodeRequest : IVonageRequest
{
    /// <summary>
    ///     The code the user supplied.
    /// </summary>
    public string Code { get; internal init; }
    
    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns></returns>
    public static IBuilderForRequestId Build() => new VerifyCodeRequestBuilder();

    /// <summary>
    ///     ID of the verify request.
    /// </summary>
    [JsonIgnore]
    public string RequestId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/verify/{this.RequestId}";

    private StringContent GetRequestContent() =>
        new(JsonSerializer.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}