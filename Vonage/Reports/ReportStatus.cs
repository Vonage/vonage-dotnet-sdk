#region
using System.ComponentModel;
#endregion

namespace Vonage.Reports;

/// <summary>
///     Represents the status of an asynchronous report request.
/// </summary>
public enum ReportStatus
{
    /// <summary>The report has been received and is waiting to be processed.</summary>
    [Description("PENDING")] Pending,

    /// <summary>The report is currently being generated.</summary>
    [Description("PROCESSING")] Processing,

    /// <summary>The report was generated successfully.</summary>
    [Description("SUCCESS")] Success,

    /// <summary>The report was aborted before completion.</summary>
    [Description("ABORTED")] Aborted,

    /// <summary>The report generation failed.</summary>
    [Description("FAILED")] Failed,

    /// <summary>The report was truncated because it exceeded the maximum allowed size.</summary>
    [Description("TRUNCATED")] Truncated,
}
