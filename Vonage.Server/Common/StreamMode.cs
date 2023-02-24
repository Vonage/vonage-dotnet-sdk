using System.ComponentModel;

namespace Vonage.Server.Common;

/// <summary>
///     Whether streams included in an archive or broadcast are selected automatically or manually.
/// </summary>
public enum StreamMode
{
    /// <summary>
    ///     All streams in the session can be included.
    /// </summary>
    [Description("auto")] Auto,

    /// <summary>
    ///     You will specify streams to be included in the archive or broadcast.
    /// </summary>
    [Description("manual")] Manual,
}