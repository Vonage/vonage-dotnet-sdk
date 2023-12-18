using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Serialization;

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
    public string GetEndpointPath() => $"/v1/meetings/themes/{this.themeId}/finalizeLogos";

    private StringContent GetRequestContent() => new(
        JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(new {Keys = new[] {this.key}}),
        Encoding.UTF8, "application/json");
}