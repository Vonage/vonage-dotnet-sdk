#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.VerifyV2.VerifyCode;

/// <inheritdoc />
[Builder]
public readonly partial struct VerifyCodeRequest : IVonageRequest
{
    /// <summary>
    ///     The code the user supplied.
    /// </summary>
    [Mandatory(1)]
    public string Code { get; internal init; }

    /// <summary>
    ///     ID of the verify request.
    /// </summary>
    [JsonIgnore]
    [Mandatory(0)]
    public Guid RequestId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/v2/verify/{this.RequestId}")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<VerifyCodeRequest> VerifyCodeNotEmpty(
        VerifyCodeRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Code, nameof(request.Code));

    [ValidationRule]
    internal static Result<VerifyCodeRequest> VerifyRequestIdNotEmpty(
        VerifyCodeRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.RequestId, nameof(request.RequestId));
}