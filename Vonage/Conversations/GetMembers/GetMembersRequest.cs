using System.Collections.Generic;
using System.Net.Http;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Conversations.GetMembers;

/// <inheritdoc />
public readonly struct GetMembersRequest : IVonageRequest
{
    /// <summary>
    ///     The cursor to start returning results from. You are not expected to provide this manually, but to follow the url
    ///     provided in _links.next.href or _links.prev.href in the response which contains a cursor value.
    /// </summary>
    public Maybe<string> Cursor { get; internal init; }
    
    /// <summary>
    ///     Defines the data ordering.
    /// </summary>
    public FetchOrder Order { get; internal init; }
    
    /// <summary>
    ///     Number of results per page.
    /// </summary>
    public int PageSize { get; internal init; }
    
    /// <summary>
    /// </summary>
    public string ConversationId { get; internal init; }
    
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();
    
    /// <inheritdoc />
    public string GetEndpointPath() => UriHelpers.BuildUri($"/v1/conversations/{this.ConversationId}/members",
        this.GetQueryStringParameters());
    
    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForConversationId Build() => new GetMembersRequestBuilder(Maybe<string>.None);
    
    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>
        {
            {"page_size", this.PageSize.ToString()},
            {"order", this.Order.AsString(EnumFormat.Description)},
        };
        this.Cursor.IfSome(value => parameters.Add("cursor", value));
        return parameters;
    }
}