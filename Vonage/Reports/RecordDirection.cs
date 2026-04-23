#region
using System.ComponentModel;
#endregion

namespace Vonage.Reports;

/// <summary>
///     Represents the direction of a communication record.
/// </summary>
public enum RecordDirection
{
    /// <summary>Received by Vonage services.</summary>
    [Description("inbound")] Inbound,

    /// <summary>Originated from Vonage services.</summary>
    [Description("outbound")] Outbound,
}
