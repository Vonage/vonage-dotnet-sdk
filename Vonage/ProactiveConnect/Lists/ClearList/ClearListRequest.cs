using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Lists.ClearList;

/// <summary>
///     Represents a request to delete all items from a list.
/// </summary>
public readonly struct ClearListRequest : IVonageRequest
{
    /// <summary>
    ///     Unique identifier for a list.
    /// </summary>
    public Guid Id { get; init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/bulk/lists/{this.Id}/clear";

    /// <summary>
    ///     Parses the input into a ClearListRequest.
    /// </summary>
    /// <param name="id">The list Id.</param>
    /// <returns>Success or Failure.</returns>
    public static Result<ClearListRequest> Parse(Guid id) => Result<ClearListRequest>
        .FromSuccess(new ClearListRequest {Id = id})
        .Bind(VerifyListId);

    private static Result<ClearListRequest> VerifyListId(ClearListRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Id, nameof(request.Id));
}