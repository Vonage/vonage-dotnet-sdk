using System;
using System.Linq;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Meetings.UpdateThemeLogo;

internal readonly struct UploadLogoRequest : IVonageRequest
{
    private UploadLogoRequest(UploadDetails fields, Uri url, byte[] file)
    {
        this.Fields = fields;
        this.Url = url;
        this.File = file;
    }

    public UploadDetails Fields { get; }

    public byte[] File { get; }

    public Uri Url { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.Url)
            .WithContent(this.GetRequestContent())
            .Build();

    public static UploadLogoRequest FromLogosUrl(GetUploadLogosUrlResponse response, byte[] file) =>
        new(response.Fields, response.Url, file);

    /// <inheritdoc />
    public string GetEndpointPath() => this.Url.AbsoluteUri;

    private MultipartFormDataContent GetRequestContent()
    {
        var content = new MultipartFormDataContent();
        this.Fields
            .ToDictionary()
            .ToList()
            .ForEach(pair => content.Add(new StringContent(pair.Value), pair.Key));
        content.Add(new ByteArrayContent(this.File), "file");
        return content;
    }
}