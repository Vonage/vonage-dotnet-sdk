#region
using System.ComponentModel;
#endregion

namespace Vonage.Reports;

/// <summary>
///     Represents the verification channel for a Verify V2 record.
/// </summary>
public enum RecordChannel
{
    /// <summary>Verify V2 standard channel.</summary>
    [Description("v2")] V2,

    /// <summary>Email verification channel.</summary>
    [Description("email")] Email,

    /// <summary>Silent authentication channel.</summary>
    [Description("silent_auth")] SilentAuth,
}
