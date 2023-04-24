using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Lists.DeleteList;

/// <summary>
///     Represents a request to delete a list.
/// </summary>
public readonly struct DeleteListRequest : IVonageRequest
{
    /// <summary>
    ///     Unique identifier for a list.
    /// </summary>
    public Guid Id { get; init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v.01/bulk/lists/{this.Id}";

    /// <summary>
    ///     Parses the input into a DeleteListRequest.
    /// </summary>
    /// <param name="id">The list Id.</param>
    /// <returns>Success or Failure.</returns>
    public static Result<DeleteListRequest> Parse(Guid id) => Result<DeleteListRequest>
        .FromSuccess(new DeleteListRequest {Id = id})
        .Bind(VerifyListId);

    private static Result<DeleteListRequest> VerifyListId(DeleteListRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Id, nameof(request.Id));
}