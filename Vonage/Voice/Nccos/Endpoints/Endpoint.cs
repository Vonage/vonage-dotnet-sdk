using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Voice.Nccos.Endpoints;

/// <summary>
///     Base class for NCCO Connect action endpoints. Represents a destination that a call can be connected to.
/// </summary>
public abstract class Endpoint
{
    /// <summary>
    ///     The type of endpoint being connected to (phone, app, websocket, sip, or vbc).
    /// </summary>
    [JsonProperty("type", Order = 99)]
    [JsonConverter(typeof(StringEnumConverter))]
    public EndpointType Type { get; protected set; }

    /// <summary>
    ///     Defines the types of endpoints that a call can be connected to via an NCCO Connect action.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EndpointType
    {
        /// <summary>
        ///     Connect to a PSTN phone number.
        /// </summary>
        [EnumMember(Value="phone")]
        Phone=1,

        /// <summary>
        ///     Connect to an in-app (Vonage Client SDK) user.
        /// </summary>
        [EnumMember(Value="app")]
        App=2,

        /// <summary>
        ///     Connect to a WebSocket for real-time audio streaming.
        /// </summary>
        [EnumMember(Value="websocket")]
        Websocket=3,

        /// <summary>
        ///     Connect to a SIP endpoint.
        /// </summary>
        [EnumMember(Value="sip")]
        Sip=4,

        /// <summary>
        ///     Connect to a Vonage Business Communications (VBC) extension.
        /// </summary>
        [EnumMember(Value="vbc")]
        Vbc=5,
    }
}