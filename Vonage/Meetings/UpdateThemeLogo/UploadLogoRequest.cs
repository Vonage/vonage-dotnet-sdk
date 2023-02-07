using System;
using System.Linq;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Meetings.UpdateThemeLogo;

internal readonly struct UploadLogoRequest : IVonageRequest
{
    private UploadLogoRequest(UploadDetails fields, Uri url, string filepath)
    {
        this.Fields = fields;
        this.Url = url;
        this.Filepath = filepath;
    }

    public UploadDetails Fields { get; }

    public string Filepath { get; }

    public Uri Url { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.Url)
            .WithContent(this.GetRequestContent())
            .Build();

    public static UploadLogoRequest FromLogosUrl(GetUploadLogosUrlResponse response, string filepath) =>
        new(response.Fields, response.Url, filepath);

    /// <inheritdoc />
    public string GetEndpointPath() => this.Url.AbsoluteUri;

    private MultipartFormDataContent GetRequestContent()
    {
        var content = new MultipartFormDataContent();
        this.Fields
            .ToDictionary()
            .ToList()
            .ForEach(pair => content.Add(new StringContent(pair.Value), pair.Key));
        content.Add(new StringContent(this.Filepath), "file");
        return content;
    }
}