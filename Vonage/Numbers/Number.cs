#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Numbers;

public class Number
{
    /// <summary>
    /// The monthly rental cost for this number, in Euros
    /// </summary>
    [JsonProperty("cost")]
    public string Cost { get; set; }

    /// <summary>
    /// The two character country code in ISO 3166-1 alpha-2 format
    /// </summary>
    [JsonProperty("country")]
    public string Country { get; set; }

    /// <summary>
    /// The capabilities of the number: SMS or VOICE or SMS,VOICE or SMS,MMS or VOICE,MMS or SMS,MMS,VOICE
    /// </summary>
    [JsonProperty("features")]
    public string[] Features { get; set; }

    /// <summary>
    /// An available inbound virtual number.
    /// </summary>
    [JsonProperty("msisdn")]
    public string Msisdn { get; set; }

    /// <summary>
    /// The type of number: landline, landline-toll-free or mobile-lvn
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    ///     The application Id.
    /// </summary>
    [JsonProperty("app_id")]
    public string ApplicationId { get; set; }

    /// <summary>
    ///     The URL of the webhook endpoint that handles inbound messages
    /// </summary>
    [JsonProperty("moHttpUrl")]
    public string MoHttpUrl { get; set; }

    /// <summary>
    ///     The messages webhook type: always app
    /// </summary>
    [JsonProperty("messagesCallbackType")]
    public string MessagesCallbackType { get; set; }

    /// <summary>
    ///     An Application ID
    /// </summary>
    [JsonProperty("messagesCallbackValue")]
    public string MessagesCallbackValue { get; set; }

    /// <summary>
    ///     The voice webhook type: sip, tel, or app
    /// </summary>
    [JsonProperty("voiceCallbackType")]
    public string VoiceCallbackType { get; set; }

    /// <summary>
    ///     A SIP URI, telephone number or Application ID
    /// </summary>
    [JsonProperty("voiceCallbackValue")]
    public string VoiceCallbackValue { get; set; }
}