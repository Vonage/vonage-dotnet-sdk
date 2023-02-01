using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.Meetings.CreateTheme;

/// <summary>
///     Represents a request to create a theme.
/// </summary>
public readonly struct CreateThemeRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    public string BrandText { get; }

    /// <summary>
    /// </summary>
    public Color MainColor { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Uri> ShortCompanyUrl { get; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> ThemeName { get; }

    internal CreateThemeRequest(string brandText, Color mainColor, Maybe<string> themeName, Maybe<Uri> shortCompanyUrl)
    {
        this.BrandText = brandText;
        this.MainColor = mainColor;
        this.ThemeName = themeName;
        this.ShortCompanyUrl = shortCompanyUrl;
    }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/beta/meetings/themes";

    private StringContent GetRequestContent() =>
        new(JsonSerializer.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}