using System;
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.ProactiveConnect.Lists.CreateList;

/// <summary>
///     Represents a request to create a list.
/// </summary>
public readonly struct CreateListRequest : IVonageRequest
{
    /// <summary>
    ///     Attributes of the list.
    /// </summary>
    public IEnumerable<ListAttribute> Attributes { get; internal init; }

    /// <summary>
    ///     The data source.
    /// </summary>
    public Maybe<ListDataSource> DataSource { get; internal init; }

    /// <summary>
    ///     The description of the resource.
    /// </summary>
    public Maybe<string> Description { get; internal init; }

    /// <summary>
    ///     The name of the resource.
    /// </summary>
    public string Name { get; internal init; }

    /// <summary>
    ///     Custom strings assigned with a resource - the request allows up to 10 tags, each must be between 1 and 15
    ///     characters.
    /// </summary>
    public IEnumerable<string> Tags { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForName Build() => new CreateListRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    /// <inheritdoc />
    public string GetEndpointPath() => "/bulk/lists";
}