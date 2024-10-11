#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.DeviceStatus.GetConnectivityStatus;

/// <summary>
/// Contains information about current connectivity status.
/// </summary>
/// <param name="Status">Represents the device status. </param>
public record GetConnectivityStatusResponse(
    [property: JsonPropertyName("connectivityStatus")]
    string Status);