using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;

namespace Vonage.Meetings.CreateTheme;

/// <summary>
///     Represents a request to create a theme.
/// </summary>
public readonly struct CreateThemeRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    public string BrandText { get; internal init; }

    /// <summary>
    /// </summary>
    public Color MainColor { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Uri> ShortCompanyUrl { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> ThemeName { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForBrand Build() => new CreateThemeRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v1/meetings/themes";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8,
            "application/json");
}