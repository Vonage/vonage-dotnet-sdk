using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Items.ExtractItems;

/// <summary>
///     Represents a request to extract items.
/// </summary>
public class ExtractItemsRequest : IVonageRequest
{
    /// <summary>
    ///     Unique identifier for the list.
    /// </summary>
    public Guid ListId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v0.1/bulk/lists/{this.ListId}/items/download";

    /// <summary>
    ///     Parses the input into a ExtractItemsRequest.
    /// </summary>
    /// <param name="id">The list Id.</param>
    /// <returns>Success or Failure.</returns>
    public static Result<ExtractItemsRequest> Parse(Guid id) => Result<ExtractItemsRequest>
        .FromSuccess(new ExtractItemsRequest {ListId = id})
        .Bind(VerifyListId);

    private static Result<ExtractItemsRequest> VerifyListId(ExtractItemsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ListId, nameof(request.ListId));
}