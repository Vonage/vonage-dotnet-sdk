#region
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using FluentAssertions;
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
    private const string From = "442079460000";
    private const string To = "447700900000";
    private const string Uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab";
    private const string ConversationId = "CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab";
    private const string Network = "1234";
    private const string Rate = "0.02";

    private static readonly DateTime StandardTimestamp = DateTime.ParseExact("2020-01-01T12:00:00.000Z",
        "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

    private static readonly DateTime StandardEndTime = DateTime.ParseExact("2020-01-01T12:00:01.000Z",
        "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

    private static Answered ExpectedAnswered =>
        new Answered
        {
            From = From,
            To = To,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
            Status = "answered",
            Direction = Direction.outbound,
            TimeStamp = StandardTimestamp,
            StartTime = StandardTimestamp,
            Network = Network,
            Rate = Rate,
        };

    private static Completed ExpectedCompleted =>
        new Completed
        {
            From = From,
            To = To,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
            Status = "completed",
            Direction = Direction.outbound,
            TimeStamp = StandardTimestamp,
            StartTime = StandardTimestamp,
            EndTime = StandardEndTime,
            Network = Network,
            Rate = Rate,
            Price = "0.03",
            Duration = "2",
            SipCode = 404,
        };

    private static Error ExpectedError =>
        new Error
        {
            Reason = "Syntax error in NCCO. Invalid value type or action.",
            ConversationUuid = ConversationId,
            TimeStamp = StandardTimestamp,
        };

    private static HumanMachine ExpectedHumanMachine =>
        new HumanMachine
        {
            From = From,
            To = To,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
            Status = "machine",
            Direction = Direction.outbound,
            TimeStamp = StandardTimestamp,
        };

    private static MultiInput ExpectedMultiInputWithDtmf =>
        new MultiInput
        {
            From = From,
            To = To,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
            TimeStamp = StandardTimestamp,
            Dtmf = new DtmfResult
            {
                Digits = "42",
                TimedOut = false,
            },
        };

    private static MultiInput ExpectedMultiInputWithSpeech =>
        new MultiInput
        {
            From = From,
            To = To,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
            TimeStamp = StandardTimestamp,
            Speech = new SpeechResult
            {
                SpeechResults = new[]
                {
                    new SpeechRecognitionResult
                    {
                        Text = "hello world",
                        Confidence = ".91",
                    },
                },
                Error = "foo",
                TimeoutReason = "bar",
            },
        };

    private static Notification<Foo> ExpectedNotification =>
        new Notification<Foo>
        {
            ConversationUuid = ConversationId,
            TimeStamp = StandardTimestamp,
            Payload = new Foo {bar = "foo"},
        };

    private static Record ExpectedRecord =>
        new Record
        {
            TimeStamp = StandardTimestamp,
            StartTime = StandardTimestamp,
            EndTime = StandardEndTime,
            RecordingUrl = "https://api.nexmo.com/v1/files/bbbbbbbb-aaaa-cccc-dddd-0123456789ab",
            Size = 12222,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
        };

    private static Transfer ExpectedTransfer =>
        new Transfer
        {
            ConversationUuidFrom = ConversationId,
            ConversationUuidTo = ConversationId,
            TimeStamp = StandardTimestamp,
            Uuid = Uuid,
        };

    private static CallStatusEvent ExpectedCallStatusEvent =>
        new CallStatusEvent
        {
            From = From,
            To = To,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
            Status = "rejected",
            DetailString = "as-yet-unknown-detail",
            Direction = Direction.outbound,
            TimeStamp = StandardTimestamp,
        };

    private static Busy ExpectedBusy =>
        new Busy
        {
            From = From,
            To = To,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
            Status = "rejected",
            DetailString = "as-yet-unknown-detail",
            Direction = Direction.outbound,
            TimeStamp = StandardTimestamp,
            SipCode = 404,
        };

    private static Failed ExpectedFailed =>
        new Failed
        {
            From = From,
            To = To,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
            Status = "rejected",
            DetailString = "as-yet-unknown-detail",
            Direction = Direction.outbound,
            TimeStamp = StandardTimestamp,
            SipCode = 404,
        };

    private static Rejected ExpectedRejected =>
        new Rejected
        {
            From = From,
            To = To,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
            Status = "rejected",
            DetailString = "as-yet-unknown-detail",
            Direction = Direction.outbound,
            TimeStamp = StandardTimestamp,
            SipCode = 404,
        };

    private static Unanswered ExpectedUnanswered =>
        new Unanswered
        {
            From = From,
            To = To,
            Uuid = Uuid,
            ConversationUuid = ConversationId,
            Status = "rejected",
            DetailString = "as-yet-unknown-detail",
            Direction = Direction.outbound,
            TimeStamp = StandardTimestamp,
            SipCode = 404,
        };

    private static void VerifyAnswer(Answer input)
    {
        input.From.Should().Be(From);
        input.To.Should().Be(To);
        input.Uuid.Should().Be(Uuid);
        input.ConversationUuid.Should().Be(ConversationId);
        input.SipHeaders.Should().BeEquivalentTo(new Dictionary<string, string>
        {
            {"SipHeader_X-Test", "test"},
            {"SipHeader_X-Value", "value"},
        });
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeAnswer(JsonSerializerType serializer)
    {
        var answer = Deserialize<Answer>(ReadJson("Voice/Data/Answer.json"), serializer);
        VerifyAnswer(answer);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeAnswered(JsonSerializerType serializer) =>
        Deserialize<Answered>(ReadJson("Voice/Data/Answered.json"), serializer).Should()
            .BeEquivalentTo(ExpectedAnswered);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeCallBusy(JsonSerializerType serializer) =>
        Deserialize<Busy>(ReadJson("Voice/Data/Busy.json"), serializer).Should().BeEquivalentTo(ExpectedBusy);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeCallFailed(JsonSerializerType serializer) =>
        Deserialize<Failed>(ReadJson("Voice/Data/Failed.json"), serializer).Should().BeEquivalentTo(ExpectedFailed);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeCallRejected(JsonSerializerType serializer) =>
        Deserialize<Rejected>(ReadJson("Voice/Data/Rejected.json"), serializer).Should()
            .BeEquivalentTo(ExpectedRejected);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeCallStatusEvent(JsonSerializerType serializer) =>
        Deserialize<CallStatusEvent>(ReadJson("Voice/Data/CallStatusEvent.json"), serializer).Should()
            .BeEquivalentTo(ExpectedCallStatusEvent);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeCallUnanswered(JsonSerializerType serializer) =>
        Deserialize<Unanswered>(ReadJson("Voice/Data/Unanswered.json"), serializer).Should()
            .BeEquivalentTo(ExpectedUnanswered);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeCompleted(JsonSerializerType serializer) =>
        Deserialize<Completed>(ReadJson("Voice/Data/Completed.json"), serializer).Should()
            .BeEquivalentTo(ExpectedCompleted);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeError(JsonSerializerType serializer) =>
        Deserialize<Error>(ReadJson("Voice/Data/Error.json"), serializer).Should().BeEquivalentTo(ExpectedError);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeHumanMachine(JsonSerializerType serializer) =>
        Deserialize<HumanMachine>(ReadJson("Voice/Data/HumanMachine.json"), serializer).Should()
            .BeEquivalentTo(ExpectedHumanMachine);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeMultiInput_WithDtmf(JsonSerializerType serializer) =>
        Deserialize<MultiInput>(ReadJson("Voice/Data/MultiInputDtmf.json"), serializer).Should()
            .BeEquivalentTo(ExpectedMultiInputWithDtmf);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeMultiInput_WithSpeech(JsonSerializerType serializer) =>
        Deserialize<MultiInput>(ReadJson("Voice/Data/MultiInputSpeech.json"), serializer).Should()
            .BeEquivalentTo(ExpectedMultiInputWithSpeech);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeNotification(JsonSerializerType serializer) =>
        Deserialize<Notification<Foo>>(ReadJson("Voice/Data/Notification.json"), serializer).Should()
            .BeEquivalentTo(ExpectedNotification);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeRecord(JsonSerializerType serializer) =>
        Deserialize<Record>(ReadJson("Voice/Data/Record.json"), serializer).Should().BeEquivalentTo(ExpectedRecord);

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeTransfer(JsonSerializerType serializer) =>
        Deserialize<Transfer>(ReadJson("Voice/Data/Transfer.json"), serializer).Should()
            .BeEquivalentTo(ExpectedTransfer);

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