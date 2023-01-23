namespace Vonage.Meetings.GetApplicationThemes;

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
    public string BrandedFaviconUrl { get; set; }

    /// <summary>
    /// </summary>
    public string BrandImageColored { get; set; }

    /// <summary>
    /// </summary>
    public string BrandImageColoredUrl { get; set; }

    /// <summary>
    /// </summary>
    public string BrandImageWhite { get; set; }

    /// <summary>
    /// </summary>
    public string BrandImageWhiteUrl { get; set; }

    /// <summary>
    /// </summary>
    public string BrandText { get; set; }

    /// <summary>
    /// </summary>
    public ThemeDomain Domain { get; set; }

    /// <summary>
    /// </summary>
    public string MainColor { get; set; }

    /// <summary>
    /// </summary>
    public string ShortCompanyUrl { get; set; }

    /// <summary>
    /// </summary>
    public string ThemeId { get; set; }

    /// <summary>
    /// </summary>
    public string ThemeName { get; set; }

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="applicationId"></param>
    /// <param name="brandedFavicon"></param>
    /// <param name="brandedFaviconUrl"></param>
    /// <param name="brandImageColored"></param>
    /// <param name="brandImageColoredUrl"></param>
    /// <param name="brandImageWhite"></param>
    /// <param name="brandImageWhiteUrl"></param>
    /// <param name="brandText"></param>
    /// <param name="domain"></param>
    /// <param name="mainColor"></param>
    /// <param name="shortCompanyUrl"></param>
    /// <param name="themeId"></param>
    /// <param name="themeName"></param>
    public Theme(string accountId, string applicationId, string brandedFavicon, string brandedFaviconUrl,
        string brandImageColored, string brandImageColoredUrl, string brandImageWhite, string brandImageWhiteUrl,
        string brandText, ThemeDomain domain, string mainColor, string shortCompanyUrl, string themeId,
        string themeName)
    {
        this.AccountId = accountId;
        this.ApplicationId = applicationId;
        this.BrandedFavicon = brandedFavicon;
        this.BrandedFaviconUrl = brandedFaviconUrl;
        this.BrandImageColored = brandImageColored;
        this.BrandImageColoredUrl = brandImageColoredUrl;
        this.BrandImageWhite = brandImageWhite;
        this.BrandImageWhiteUrl = brandImageWhiteUrl;
        this.BrandText = brandText;
        this.Domain = domain;
        this.MainColor = mainColor;
        this.ShortCompanyUrl = shortCompanyUrl;
        this.ThemeId = themeId;
        this.ThemeName = themeName;
    }
}