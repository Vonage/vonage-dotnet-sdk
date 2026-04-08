#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Voice.Emergency.GetAddress;

/// <inheritdoc />
[Builder]
public readonly partial struct GetAddressRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the unique identifier (UUID) of the address to retrieve.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public Guid Id { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, $"/v1/addresses/{this.Id}")
            .Build();

    [ValidationRule]
    internal static Result<GetAddressRequest> VerifyAddressId(GetAddressRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Id, nameof(request.Id));
}