using System;
using System.IO.Abstractions;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.ProactiveConnect.Items.ImportItems;

/// <summary>
///     Represents a request to import items from CSV file.
/// </summary>
public class ImportItemsRequest : IVonageRequest
{
    /// <summary>
    ///     CSV content of list items to import.
    /// </summary>
    public byte[] File { get; internal init; }

    /// <summary>
    ///     Unique identifier for a list.
    /// </summary>
    public Guid ListId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForListId Build() => new ImportItemsRequestBuilder(new FileSystem().File);

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v0.1/bulk/lists/{this.ListId}/items/import";

    private MultipartFormDataContent GetRequestContent()
    {
        var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(this.File), "file");
        return content;
    }
}