using System;
using System.Drawing;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.Meetings.Common;

/// <summary>
/// </summary>
public struct Theme
{
    /// <summary>
    /// </summary>
    public string AccountId { get; set; }

    /// <summary>
    /// </summary>
    public string ApplicationId { get; set; }

    /// <summary>
    /// </summary>
    public string BrandedFavicon { get; set; }

    /// <summary>
    /// </summary>
    public Uri BrandedFaviconUrl { get; set; }

    /// <summary>
    /// </summary>
    public string BrandImageColored { get; set; }

    /// <summary>
    /// </summary>
    public Uri BrandImageColoredUrl { get; set; }

    /// <summary>
    /// </summary>
    public string BrandImageWhite { get; set; }

    /// <summary>
    /// </summary>
    public Uri BrandImageWhiteUrl { get; set; }

    /// <summary>
    /// </summary>
    public string BrandText { get; set; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<ThemeDomain>))]
    public ThemeDomain Domain { get; set; }

    /// <summary>
    /// </summary>
    public Color MainColor { get; set; }

    /// <summary>
    /// </summary>
    public Uri ShortCompanyUrl { get; set; }

    /// <summary>
    /// </summary>
    public string ThemeId { get; set; }

    /// <summary>
    /// </summary>
    public string ThemeName { get; set; }
}