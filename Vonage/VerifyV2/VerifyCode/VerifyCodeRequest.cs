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

/// <summary>
///     Represents a request to verify a PIN code submitted by a user against an active verification request. The code can be retried up to 3 times before the request is locked.
/// </summary>
[Builder]
public readonly partial struct VerifyCodeRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the PIN code provided by the user. Must be 4-10 alphanumeric characters matching the code sent to the user.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCode("1234")
    /// ]]></code>
    /// </example>
    [Mandatory(1)]
    public string Code { get; internal init; }

    /// <summary>
    ///     Sets the unique identifier (UUID) of the verification request to validate against.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithRequestId(Guid.Parse("c11236f4-00bf-4b89-84ba-88b25df97315"))
    /// ]]></code>
    /// </example>
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