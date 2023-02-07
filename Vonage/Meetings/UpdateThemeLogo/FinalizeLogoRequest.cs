using System;
using System.Net.Http;
using System.Text;
using Vonage.Common;
using Vonage.Common.Client;

namespace Vonage.Meetings.UpdateThemeLogo;

internal readonly struct FinalizeLogoRequest : IVonageRequest
{
    private readonly Guid themeId;
    private readonly string key;

    public FinalizeLogoRequest(Guid themeId, string key)
    {
        this.themeId = themeId;
        this.key = key;
    }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Put, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/beta/meetings/themes/{this.themeId}/finalizeLogos";

    private StringContent GetRequestContent() => new(
        JsonSerializer.BuildWithSnakeCase().SerializeObject(new {Keys = new[] {this.key}}),
        Encoding.UTF8, "application/json");
}