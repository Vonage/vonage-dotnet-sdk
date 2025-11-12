#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice;

/// <summary>
/// </summary>
/// <param name="WordText"></param>
/// <param name="StartTime"></param>
/// <param name="EndTime"></param>
/// <param name="Confidence"></param>
public record Word(
    [property: JsonProperty("word")]
    [property: JsonPropertyName("word")]
    string WordText,
    [property: JsonProperty("start_time")]
    [property: JsonPropertyName("start_time")]
    int StartTime,
    [property: JsonProperty("end_time")]
    [property: JsonPropertyName("end_time")]
    int EndTime,
    [property: JsonProperty("confidence")]
    [property: JsonPropertyName("confidence")]
    double Confidence
);

/// <summary>
/// </summary>
/// <param name="Sentence"></param>
/// <param name="RawSentence"></param>
/// <param name="Duration"></param>
/// <param name="Timestamp"></param>
/// <param name="Words"></param>
public record Transcript(
    [property: JsonProperty("sentence")]
    [property: JsonPropertyName("sentence")]
    string Sentence,
    [property: JsonProperty("raw_sentence")]
    [property: JsonPropertyName("raw_sentence")]
    string RawSentence,
    [property: JsonProperty("duration")]
    [property: JsonPropertyName("duration")]
    int Duration,
    [property: JsonProperty("timestamp")]
    [property: JsonPropertyName("timestamp")]
    int Timestamp,
    [property: JsonProperty("words")]
    [property: JsonPropertyName("words")]
    IReadOnlyList<Word> Words
);

/// <summary>
/// </summary>
/// <param name="Transcript"></param>
/// <param name="Duration"></param>
public record Channel(
    [property: JsonProperty("transcript")]
    [property: JsonPropertyName("transcript")]
    IReadOnlyList<Transcript> Transcript,
    [property: JsonProperty("duration")]
    [property: JsonPropertyName("duration")]
    int Duration
)
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    public string ExtractTranscript() => this.Transcript
        .Aggregate(new StringBuilder(), (builder, transcript) => builder.AppendLine(transcript.Sentence)).ToString();
}

/// <summary>
/// </summary>
/// <param name="Version"></param>
/// <param name="RequestId"></param>
/// <param name="Channels"></param>
public record TranscriptionResult(
    [property: JsonProperty("ver")]
    [property: JsonPropertyName("ver")]
    string Version,
    [property: JsonProperty("request_id")]
    [property: JsonPropertyName("request_id")]
    Guid RequestId,
    [property: JsonProperty("channels")]
    [property: JsonPropertyName("channels")]
    IReadOnlyList<Channel> Channels
)
{
    /// <summary>
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> ExtractTranscripts() => this.Channels.Select(channel => channel.ExtractTranscript());
}