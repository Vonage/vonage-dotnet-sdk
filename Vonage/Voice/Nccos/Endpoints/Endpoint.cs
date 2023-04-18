using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Voice.Nccos.Endpoints;

public abstract class Endpoint
{
    /// <summary>
    /// the type of endpoint being connected
    /// </summary>
    [JsonProperty("type", Order = 99)]
    [JsonConverter(typeof(StringEnumConverter))]
    public EndpointType Type { get; protected set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum EndpointType
    {
        [EnumMember(Value="phone")]
        Phone=1,
        [EnumMember(Value="app")]
        App=2,
        [EnumMember(Value="websocket")]
        Websocket=3,
        [EnumMember(Value="sip")]
        Sip=4,
        [EnumMember(Value="vbc")]
        Vbc=5,
    }
}