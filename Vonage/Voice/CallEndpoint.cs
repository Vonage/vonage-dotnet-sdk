﻿using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Vonage.Voice;

public class CallEndpoint
{
    // websocket:

    /// <summary>
    /// The internet media type for the audio you are streaming.Possible values are: audio/l16; rate=16000
    /// </summary>
    [JsonProperty("content-type", Order = 2)]
    public string ContentType { get; set; }

    /// <summary>
    /// Set the digits that are sent to the user as soon as the Call is answered. The * and # digits are respected. You create pauses using p. Each pause is 500ms.
    /// </summary>
    [JsonProperty("dtmfAnswer")]
    public string DtmfAnswer { get; set; }

    /// <summary>
    /// A JSON object containing any metadata you want.
    /// </summary>
    [JsonProperty("headers", Order = 3)]
    public object Headers { get; set; }

    /// <summary>
    /// The phone number to connect to in E.164 format.
    /// </summary>
    [JsonProperty("number")]
    public string Number { get; set; }

    /// <summary>
    /// One of the following: phone, websocket, sip
    /// </summary>
    [JsonProperty("type", Order = 0)]
    public string Type { get; set; }

    /// <summary>
    /// The URI to the websocket you are streaming to.
    /// OR
    /// The SIP URI to the endpoint you are connecting to in the format sip:rebekka@sip.example.com.
    /// </summary>
    [JsonProperty("uri", Order = 1)]
    public string Uri { get; set; }
}