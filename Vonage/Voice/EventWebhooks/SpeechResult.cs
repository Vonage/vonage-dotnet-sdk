﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Voice.EventWebhooks;

public class SpeechResult
{
    /// <summary>
    /// Indicates if the input ended when user stopped speaking (end_on_silence_timeout), by max duration timeout (max_duration) or if the user didn't say anything (start_timeout)
    /// </summary>
    [JsonProperty("timeout_reason")]
    public string TimeoutReason { get; set; }

    /// <summary>
    /// Error field in case there was a problem during speech recognition - will not be present if nothing went wrong.
    /// </summary>
    [JsonProperty("error")]
    public string Error { get; set; }

    /// <summary>
    /// Array of SpeechRecognitionResults
    /// </summary>
    [JsonProperty("results")]
    public SpeechRecognitionResult[] SpeechResults { get; set; }
}