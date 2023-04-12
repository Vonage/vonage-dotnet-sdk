using Newtonsoft.Json;

namespace Vonage.Voice.Nccos.Endpoints;

public class PhoneEndpoint : Endpoint
{
    /// <summary>
    /// The phone number to connect to in E.164 format.
    /// </summary>
    [JsonProperty("number")]
    public string Number { get; set; }

    /// <summary>
    /// Set the digits that are sent to the user as soon as the Call is answered. 
    /// The * and # digits are respected. You create pauses using p. Each pause is 500ms.
    /// </summary>
    [JsonProperty("dtmfAnswer")]
    public string DtmfAnswer { get; set; }

    /// <summary>
    /// An object containing a required url key. 
    /// The URL serves an NCCO to execute in the number being connected to, 
    /// before that call is joined to your existing conversation. 
    /// Optionally, the ringbackTone key can be specified with a URL value that points to a 
    /// ringbackTone to be played back on repeat to the caller, so they do not hear just silence. 
    /// The ringbackTone will automatically stop playing when the call is fully connected. 
    /// Example: {"url":"https://example.com/answer", "ringbackTone":"http://example.com/ringbackTone.wav" }.
    /// Please note, the key ringback is still supported.
    /// </summary>
    [JsonProperty("onAnswer")]
    public Answer OnAnswer { get; set; }

    public PhoneEndpoint()
    {
        this.Type = EndpointType.Phone;
    }
        
    public class Answer
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("ringbackTone")]
        public string RingbackTone { get; set; }
    }
}