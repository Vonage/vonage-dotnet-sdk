#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.Nccos.Endpoints;

/// <summary>
/// </summary>
public class PhoneEndpoint : Endpoint
{
    /// <summary>
    /// </summary>
    public PhoneEndpoint() => this.Type = EndpointType.Phone;

    /// <summary>
    ///     The phone number to connect to in E.164 format.
    /// </summary>
    [JsonProperty("number", Order = 0)]
    public string Number { get; set; }

    /// <summary>
    ///     Set the digits that are sent to the user as soon as the Call is answered.
    ///     The * and # digits are respected. You create pauses using p. Each pause is 500ms.
    /// </summary>
    [JsonProperty("dtmfAnswer", Order = 1)]
    public string DtmfAnswer { get; set; }

    /// <summary>
    ///     An object containing a required url key.
    ///     The URL serves an NCCO to execute in the number being connected to,
    ///     before that call is joined to your existing conversation.
    ///     Optionally, the ringbackTone key can be specified with a URL value that points to a
    ///     ringbackTone to be played back on repeat to the caller, so they do not hear just silence.
    ///     The ringbackTone will automatically stop playing when the call is fully connected.
    ///     Example: {"url":"https://example.com/answer", "ringbackTone":"http://example.com/ringbackTone.wav" }.
    ///     Please note, the key ringback is still supported.
    /// </summary>
    [JsonProperty("onAnswer", Order = 2)]
    public Answer OnAnswer { get; set; }

    /// <summary>
    ///     For Vonage customers who are required by the FCC to sign their own calls to the USA, we allow you to place Voice
    ///     API calls with your own signature. This feature is available by request only. Please be aware calls with an invalid
    ///     signature will be rejected. For more information please contact us. In this option, you must place the STIR/SHAKEN
    ///     Identity Header content that Vonage must use for this call. Expected format is composed of the JWT with the header,
    ///     payload and signature, an info parameter with a link for the certificate, the algorithm (alg) parameter indicating
    ///     which encryption type was used and the passport type (ppt) which should be shaken.
    /// </summary>
    [JsonProperty("shaken", Order = 3)]
    public string Shaken { get; set; }

    /// <summary>
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("ringbackTone")]
        public string RingbackTone { get; set; }
    }
}