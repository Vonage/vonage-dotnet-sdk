using System.ComponentModel;

namespace Vonage.Meetings.Common;

/// <summary>
/// </summary>
public enum RecordingStatus
{
    /// <summary>
    /// </summary>
    [Description("started")] Started,

    /// <summary>
    /// </summary>
    [Description("stopped")] Stopped,

    /// <summary>
    /// </summary>
    [Description("paused")] Paused,

    /// <summary>
    /// </summary>
    [Description("uploaded")] Uploaded,
}