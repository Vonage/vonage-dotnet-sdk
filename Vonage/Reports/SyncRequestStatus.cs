#region
using System.ComponentModel;
#endregion

namespace Vonage.Reports;

/// <summary>
///     Represents the result status of a synchronous records request.
/// </summary>
public enum SyncRequestStatus
{
    /// <summary>All records were returned successfully.</summary>
    [Description("SUCCESS")] Success,

    /// <summary>The result was truncated because it exceeded the maximum allowed size.</summary>
    [Description("TRUNCATED")] Truncated,
}
