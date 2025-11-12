#region
using System;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Voice;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;
#endregion

namespace Vonage.Test.Voice;

public class TranscriptionModelsTest
{
    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeTranscriptionResult(JsonSerializerType serializer)
    {
        var result = Deserialize<TranscriptionResult>(ReadJson("Voice/Data/TranscriptionSample.json"), serializer);
        result.RequestId.Should().Be("57c382a2-984d-4d67-87de-eafe2c8f35e3");
        result.Version.Should().Be("2.1.0");
        result.Channels.Should().HaveCount(1);
        result.Channels[0].Duration.Should().Be(1);
        result.Channels[0].Transcript.Should().HaveCount(1);
        result.Channels[0].Transcript[0].Duration.Should().Be(1120);
        result.Channels[0].Transcript[0].Timestamp.Should().Be(1640);
        result.Channels[0].Transcript[0].RawSentence.Should().Be("is two two one two");
        result.Channels[0].Transcript[0].Sentence.Should().Be("Is 2212.");
        result.Channels[0].Transcript[0].Words.Should().HaveCount(2);
        result.Channels[0].Transcript[0].Words[0].Should().Be(new Word("Is", 1640, 1720, 0.513033));
        result.Channels[0].Transcript[0].Words[1].Should().Be(new Word("2212", 1800, 2760, 0.513033));
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ExtractTranscripts(JsonSerializerType serializer)
    {
        var result = Deserialize<TranscriptionResult>(ReadJson("Voice/Data/TranscriptionSample.json"), serializer);
        result.ExtractTranscripts().Should().BeEquivalentTo("Is 2212.");
    }

    private static T Deserialize<T>(string json, JsonSerializerType serializerType) => serializerType switch
    {
        JsonSerializerType.Newtonsoft => JsonConvert.DeserializeObject<T>(json),
        JsonSerializerType.SystemTextJson => JsonSerializer.Deserialize<T>(json),
        _ => throw new ArgumentOutOfRangeException(nameof(serializerType)),
    };

    private static string ReadJson(string path) => File.ReadAllText(path);
}