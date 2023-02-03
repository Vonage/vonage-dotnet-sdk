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
    /// The favicon key in storage system
    /// </summary>
    public string BrandedFavicon { get; set; }

    /// <summary>
    /// The favicon link.
    /// </summary>
    public Uri BrandedFaviconUrl { get; set; }

    /// <summary>
    /// Colored logo's key in storage system.
    /// </summary>
    public string BrandImageColored { get; set; }

    /// <summary>
    /// Colored logo's link.
    /// </summary>
    public Uri BrandImageColoredUrl { get; set; }

    /// <summary>
    /// White logo's key in storage system.
    /// </summary>
    public string BrandImageWhite { get; set; }

    /// <summary>
    /// White logo's link.
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