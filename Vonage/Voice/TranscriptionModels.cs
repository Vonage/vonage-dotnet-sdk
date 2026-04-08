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
///     Represents a single word within a transcription, including timing and confidence information.
/// </summary>
/// <param name="WordText">The transcribed word text.</param>
/// <param name="StartTime">The start time of the word in milliseconds from the beginning of the audio.</param>
/// <param name="EndTime">The end time of the word in milliseconds from the beginning of the audio.</param>
/// <param name="Confidence">The confidence score for the transcribed word (0.0 to 1.0).</param>
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
///     Represents a single transcript segment within a channel, containing the recognized sentence and word-level details.
/// </summary>
/// <param name="Sentence">The processed transcription sentence text.</param>
/// <param name="RawSentence">The raw, unprocessed transcription sentence text.</param>
/// <param name="Duration">The duration of the transcript segment in milliseconds.</param>
/// <param name="Timestamp">The start timestamp of the transcript segment in milliseconds from the beginning of the audio.</param>
/// <param name="Words">The individual words in the transcript with timing and confidence information.</param>
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
///     Represents a single audio channel in a transcription result, containing the transcript segments and total duration.
/// </summary>
/// <param name="Transcript">The list of transcript segments for this channel.</param>
/// <param name="Duration">The total duration of the audio channel in milliseconds.</param>
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
    ///     Concatenates all transcript sentences in this channel into a single string.
    /// </summary>
    /// <returns>The full transcript text for this channel.</returns>
    public string ExtractTranscript() => this.Transcript
        .Aggregate(new StringBuilder(), (builder, transcript) => builder.Append(transcript.Sentence)).ToString();
}

/// <summary>
///     Represents the complete transcription result from a voice call recording, containing all channels and their transcripts.
/// </summary>
/// <param name="Version">The version of the transcription format.</param>
/// <param name="RequestId">The unique identifier for the transcription request.</param>
/// <param name="Channels">The list of audio channels, each containing transcript segments.</param>
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
    ///     Extracts the full transcript text from each channel as a collection of strings.
    /// </summary>
    /// <returns>An enumerable of transcript strings, one per channel.</returns>
    public IEnumerable<string> ExtractTranscripts() => this.Channels.Select(channel => channel.ExtractTranscript());
}