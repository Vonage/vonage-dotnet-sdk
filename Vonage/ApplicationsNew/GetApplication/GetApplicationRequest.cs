using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ApplicationsNew.GetApplication;

/// <summary>
///     Represents a request to retrieve a specific application by its identifier.
/// </summary>
[Builder]
public readonly partial struct GetApplicationRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the unique identifier of the application to retrieve.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public string ApplicationId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, $"/v2/applications/{this.ApplicationId}")
        .Build();

    [ValidationRule]
    internal static Result<GetApplicationRequest> VerifyApplicationId(GetApplicationRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));
}
