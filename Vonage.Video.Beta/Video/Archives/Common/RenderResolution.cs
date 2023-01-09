using System.ComponentModel;

namespace Vonage.Video.Beta.Video.Archives.Common;

/// <summary>
///     Enum for representing resolutions with orientation.
/// </summary>
public enum RenderResolution
{
    /// <summary>
    ///     Standard definition (SD) resolution with landscape orientation (640x480)
    /// </summary>
    [Description("640x480")] StandardDefinitionLandscape,

    /// <summary>
    ///     Standard definition (SD) resolution with portrait orientation (480x640)
    /// </summary>
    [Description("480x640")] StandardDefinitionPortrait,

    /// <summary>
    ///     High definition (HD) resolution with landscape orientation (1280x780)
    /// </summary>
    [Description("1280x720")] HighDefinitionLandscape,

    /// <summary>
    ///     High definition (HD) resolution with portrait orientation (780x1280)
    /// </summary>
    [Description("720x1280")] HighDefinitionPortrait,

    /// <summary>
    ///     Full high definition (FHD) resolution with landscape orientation (1920x1080)
    /// </summary>
    [Description("1920x1080")] FullHighDefinitionLandscape,

    /// <summary>
    ///     Full high definition (FHD) resolution with portrait orientation (1080x1920)
    /// </summary>
    [Description("1080x1920")] FullHighDefinitionPortrait,
}