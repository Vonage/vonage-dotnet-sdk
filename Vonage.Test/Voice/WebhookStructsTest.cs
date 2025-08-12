#region
using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Vonage.Voice.AnswerWebhooks;
using Vonage.Voice.EventWebhooks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Record = Vonage.Voice.EventWebhooks.Record;
#endregion

namespace Vonage.Test.Voice;

[Trait("Category", "Legacy")]
public class WebhookStructsTest
{
    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeAnswer(JsonSerializerType serializer)
    {
        var webhook = Deserialize<Answer>(ReadJson("Voice/Data/Answer.json"), serializer);
        Assert.Equal("442079460000", webhook.From);
        Assert.Equal("447700900000", webhook.To);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.Uuid);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuid);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeAnswered(JsonSerializerType serializer)
    {
        var webhook = Deserialize<Answered>(ReadJson("Voice/Data/Answered.json"), serializer);
        Assert.Equal("442079460000", webhook.From);
        Assert.Equal("447700900000", webhook.To);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.Uuid);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuid);
        Assert.Equal("answered", webhook.Status);
        Assert.Equal(Direction.outbound, webhook.Direction);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.TimeStamp);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.StartTime);
        Assert.Equal("1234", webhook.Network);
        Assert.Equal("0.02", webhook.Rate);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeCallStatusEvent(JsonSerializerType serializer)
    {
        var webhook = Deserialize<CallStatusEvent>(ReadJson("Voice/Data/CallStatusEvent.json"), serializer);
        Assert.Equal("442079460000", webhook.From);
        Assert.Equal("447700900000", webhook.To);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.Uuid);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuid);
        Assert.Equal("rejected", webhook.Status);
        Assert.Equal(DetailedStatus.unmapped_detail, webhook.Detail);
        Assert.Equal("as-yet-unknown-detail", webhook.DetailString);
        Assert.Equal(Direction.outbound, webhook.Direction);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.TimeStamp);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeCompleted(JsonSerializerType serializer)
    {
        var webhook = Deserialize<Completed>(ReadJson("Voice/Data/Completed.json"), serializer);
        Assert.Equal("442079460000", webhook.From);
        Assert.Equal("447700900000", webhook.To);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.Uuid);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuid);
        Assert.Equal("completed", webhook.Status);
        Assert.Equal(Direction.outbound, webhook.Direction);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.TimeStamp);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.StartTime);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:01.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.EndTime);
        Assert.Equal("1234", webhook.Network);
        Assert.Equal("0.02", webhook.Rate);
        Assert.Equal("0.03", webhook.Price);
        Assert.Equal("2", webhook.Duration);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeError(JsonSerializerType serializer)
    {
        var webhook = Deserialize<Error>(ReadJson("Voice/Data/Error.json"), serializer);
        Assert.Equal("Syntax error in NCCO. Invalid value type or action.", webhook.Reason);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuid);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.TimeStamp);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeHumanMachine(JsonSerializerType serializer)
    {
        var webhook = Deserialize<HumanMachine>(ReadJson("Voice/Data/HumanMachine.json"), serializer);
        Assert.Equal("442079460000", webhook.From);
        Assert.Equal("447700900000", webhook.To);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.Uuid);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuid);
        Assert.Equal("machine", webhook.Status);
        Assert.Equal(Direction.outbound, webhook.Direction);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.TimeStamp);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeMultiInput_WithDtmf(JsonSerializerType serializer)
    {
        var webhook = Deserialize<MultiInput>(ReadJson("Voice/Data/MultiInputDtmf.json"), serializer);
        Assert.Equal("442079460000", webhook.From);
        Assert.Equal("447700900000", webhook.To);
        Assert.Equal("42", webhook.Dtmf.Digits);
        Assert.False(webhook.Dtmf.TimedOut);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.Uuid);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuid);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.TimeStamp);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeMultiInput_WithSpeech(JsonSerializerType serializer)
    {
        var webhook = Deserialize<MultiInput>(ReadJson("Voice/Data/MultiInputSpeech.json"), serializer);
        Assert.Equal("442079460000", webhook.From);
        Assert.Equal("447700900000", webhook.To);
        Assert.Equal("hello world", webhook.Speech.SpeechResults[0].Text);
        Assert.Equal(".91", webhook.Speech.SpeechResults[0].Confidence);
        Assert.Equal("foo", webhook.Speech.Error);
        Assert.Equal("bar", webhook.Speech.TimeoutReason);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.Uuid);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuid);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.TimeStamp);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeNotification(JsonSerializerType serializer)
    {
        var webhook = Deserialize<Notification<Foo>>(ReadJson("Voice/Data/Notification.json"), serializer);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuid);
        Assert.Equal("foo", webhook.Payload.bar);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.TimeStamp);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeRecord(JsonSerializerType serializer)
    {
        var webhook = Deserialize<Record>(ReadJson("Voice/Data/Record.json"), serializer);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.TimeStamp);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.StartTime);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:01.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.EndTime);
        Assert.Equal("https://api.nexmo.com/v1/files/bbbbbbbb-aaaa-cccc-dddd-0123456789ab",
            webhook.RecordingUrl);
        Assert.True(12222 == webhook.Size);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.Uuid);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuid);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeTransfer(JsonSerializerType serializer)
    {
        var webhook = Deserialize<Transfer>(ReadJson("Voice/Data/Transfer.json"), serializer);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuidFrom);
        Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.ConversationUuidTo);
        Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                          DateTimeStyles.AdjustToUniversal), webhook.TimeStamp);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.Uuid);
    }

    private static T Deserialize<T>(string json, JsonSerializerType serializerType) => serializerType switch
    {
        JsonSerializerType.Newtonsoft => JsonConvert.DeserializeObject<T>(json),
        JsonSerializerType.SystemTextJson => JsonSerializer.Deserialize<T>(json),
        _ => throw new ArgumentOutOfRangeException(nameof(serializerType)),
    };

    private static string ReadJson(string path) => File.ReadAllText(path);

    public class Foo
    {
        public string bar { get; set; }
    }
}