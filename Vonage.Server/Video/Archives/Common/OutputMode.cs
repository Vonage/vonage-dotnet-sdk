using System.ComponentModel;

namespace Vonage.Server.Video.Archives.Common;

/// <summary>
///     Defines values for the OutputMode property of an Archive object.
/// </summary>
public enum OutputMode
{
    /// <summary>
    ///     All streams in the archive are recorded to a single (composed) file.
    /// </summary>
    [Description("composed")] Composed,

    /// <summary>
    ///     Each stream in the archive is recorded to its own individual file.
    /// </summary>
    [Description("individual")] Individual,
}