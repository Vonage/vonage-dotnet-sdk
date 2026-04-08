#region
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.VerifyV2.CreateTemplate;

/// <summary>
///     Represents a request to create a new custom verification template. Templates allow customization of the message text sent to users during verification.
/// </summary>
[Builder]
public readonly partial struct CreateTemplateRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the reference name for the template. Must be 1-64 characters matching the pattern ^[A-Za-z0-9_-]+$ and unique within the account.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithName("my-custom-template")
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public string Name { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "/v2/verify/templates")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<CreateTemplateRequest> VerifyNameNotEmpty(
        CreateTemplateRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Name, nameof(request.Name));
}