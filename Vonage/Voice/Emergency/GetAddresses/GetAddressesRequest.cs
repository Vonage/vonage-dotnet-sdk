#region
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Voice.Emergency.GetAddresses;

/// <inheritdoc />
[Builder]
public readonly partial struct GetAddressesRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    [OptionalWithDefault("int", "1")]
    public int Page { get; internal init; }

    /// <summary>
    /// </summary>
    [OptionalWithDefault("int", "100")]
    public int PageSize { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    private string GetEndpointPath() => UriHelpers.BuildUri("/v1/addresses", this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters() => new Dictionary<string, string>
    {
        {"page", this.Page.ToString()},
        {"page_size", this.PageSize.ToString()},
    };

    [ValidationRule]
    internal static Result<GetAddressesRequest> VerifyMinPage(GetAddressesRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.Page, 1, nameof(request.Page));

    [ValidationRule]
    internal static Result<GetAddressesRequest> VerifyMinPageSize(GetAddressesRequest request) =>
        InputValidation.VerifyHigherOrEqualThan(request, request.PageSize, 1, nameof(request.PageSize));

    [ValidationRule]
    internal static Result<GetAddressesRequest> VerifyMaxPageSize(GetAddressesRequest request) =>
        InputValidation.VerifyLowerOrEqualThan(request, request.PageSize, 1000, nameof(request.PageSize));
}