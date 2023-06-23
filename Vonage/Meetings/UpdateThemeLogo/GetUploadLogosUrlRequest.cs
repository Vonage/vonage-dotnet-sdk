using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Meetings.UpdateThemeLogo;

internal readonly struct GetUploadLogosUrlRequest : IVonageRequest
{
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/meetings/themes/logos-upload-urls";

    internal static GetUploadLogosUrlRequest Default => new();
}