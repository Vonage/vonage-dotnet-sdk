using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Lists.GetList;

/// <summary>
///     Represents a request to retrieve a single list.
/// </summary>
public readonly struct GetListRequest : IVonageRequest
{
    /// <summary>
    ///     Unique identifier for a list.
    /// </summary>
    public Guid Id { get; init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v.01/bulk/lists/{this.Id}";

    /// <summary>
    ///     Parses the input into a GetListRequest.
    /// </summary>
    /// <param name="id">The list Id.</param>
    /// <returns>Success or Failure.</returns>
    public static Result<GetListRequest> Parse(Guid id) => Result<GetListRequest>
        .FromSuccess(new GetListRequest {Id = id})
        .Bind(VerifyListId);

    private static Result<GetListRequest> VerifyListId(GetListRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Id, nameof(request.Id));
}