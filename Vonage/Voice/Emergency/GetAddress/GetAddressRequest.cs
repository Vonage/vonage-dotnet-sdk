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
    /// </summary>
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