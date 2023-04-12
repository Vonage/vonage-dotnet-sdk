using System;
using Newtonsoft.Json;

namespace Vonage.Numbers;

public class UpdateNumberRequest
{
    /// <summary>
    /// The two character country code in ISO 3166-1 alpha-2 format
    /// </summary>
    [JsonProperty("country")]
    public string Country { get; set; }

    /// <summary>
    /// An available inbound virtual number.
    /// </summary>
    [JsonProperty("msisdn")]
    public string Msisdn { get; set; }

    /// <summary>
    /// The Application that will handle inbound traffic to this number.
    /// </summary>
    [JsonProperty("app_id")]
    public string AppId { get; set; }

    /// <summary>
    /// An URL-encoded URI to the webhook endpoint that handles inbound messages. 
    /// Your webhook endpoint must be active before you make this request. 
    /// Vonage makes a GET request to the endpoint and checks that it returns a 200 OK response. 
    /// Set this parameter's value to an empty string to remove the webhook.
    /// </summary>
    [JsonProperty("moHttpUrl")]
    public string MoHttpUrl { get; set; }

    /// <summary>
    /// The associated system type for your SMPP client
    /// </summary>
    [JsonProperty("moSmppSysType")]
    public string MoSmppSysType { get; set; }

    /// <summary>
    /// Specify whether inbound voice calls on your number are forwarded to a SIP or a telephone number. 
    /// This must be used with the voiceCallbackValue parameter. 
    /// If set, sip or tel are prioritized over the Voice capability in your Application. 
    /// Note: The app value is deprecated and will be removed in future.
    /// </summary>
    [JsonProperty("voiceCallbackType")]
    public string VoiceCallbackType { get; set; }

    /// <summary>
    /// A SIP URI or telephone number. Must be used with the voiceCallbackType parameter.
    /// </summary>
    [JsonProperty("voiceCallbackValue")]
    public string VoiceCallbackValue { get; set; }

    /// <summary>
    /// A webhook URI for Vonage to send a request to when a call ends
    /// </summary>
    [JsonProperty("voiceStatusCallback")]
    public string VoiceStatusCallback { get; set; }
}