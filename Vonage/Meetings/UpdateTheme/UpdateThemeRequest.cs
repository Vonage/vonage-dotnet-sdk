using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.Meetings.UpdateTheme;

/// <summary>
///     Represents a request to update an existing theme.
/// </summary>
public readonly struct UpdateThemeRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> BrandText { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<Color>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Color> MainColor { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Uri> ShortCompanyUrl { get; init; }

    /// <summary>
    /// </summary>
    [JsonIgnore]
    public string ThemeId { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> ThemeName { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/beta/meetings/themes/{this.ThemeId}";

    private StringContent GetRequestContent()
    {
        var serialized =
            this.HasUpdateContent()
                ? JsonSerializer.BuildWithSnakeCase().SerializeObject(new {UpdateDetails = this})
                : JsonSerializer.BuildWithSnakeCase().SerializeObject(new { });
        return new StringContent(serialized, Encoding.UTF8, "application/json");
    }

    private bool HasUpdateContent() => this.MainColor.IsSome || this.BrandText.IsSome || this.ThemeName.IsSome ||
                                       this.ShortCompanyUrl.IsSome;
}