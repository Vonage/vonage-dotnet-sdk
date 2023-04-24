using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Lists.ReplaceItems;

/// <summary>
///     Represents a request to replace all items in a list.
/// </summary>
public readonly struct ReplaceItemsRequest : IVonageRequest
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
    public string GetEndpointPath() => $"/bulk/lists/{this.Id}/fetch";

    /// <summary>
    ///     Parses the input into a ReplaceItemsRequest.
    /// </summary>
    /// <param name="id">The list Id.</param>
    /// <returns>Success or Failure.</returns>
    public static Result<ReplaceItemsRequest> Parse(Guid id) => Result<ReplaceItemsRequest>
        .FromSuccess(new ReplaceItemsRequest {Id = id})
        .Bind(VerifyListId);

    private static Result<ReplaceItemsRequest> VerifyListId(ReplaceItemsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Id, nameof(request.Id));
}