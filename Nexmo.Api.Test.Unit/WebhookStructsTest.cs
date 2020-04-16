using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Nexmo.Api.Voice.EventWebhooks;
using Nexmo.Api.Voice.AnswerWebhooks;
using Newtonsoft.Json;
using System.Globalization;

namespace Nexmo.Api.Test.Unit
{
    public class WebhookStructsTest
    {
        [Fact]
        public void TestAnswer()
        {
            var json = @"{
                  ""from"": ""442079460000"",
                  ""to"": ""447700900000"",
                  ""uuid"": ""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                  ""conversation_uuid"": ""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab""
                }";

            var answerWebhook = JsonConvert.DeserializeObject<Answer>(json);
            Assert.Equal("442079460000", answerWebhook.From);
            Assert.Equal("447700900000", answerWebhook.To);
            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", answerWebhook.Uuid);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", answerWebhook.ConversationUuid);
        }        
 
        [Theory]
        [InlineData("disconnected")]
        [InlineData("cancelled")]
        [InlineData("busy")]
        [InlineData("ringing")]
        [InlineData("started")]
        [InlineData("timeout")]
        [InlineData("rejected")]
        [InlineData("failed")]
        [InlineData("unanswered")]
        public void TestCallStatusEvent(string type)
        {
            var json = @"
                {
                    ""from"":""442079460000"", 
                    ""to"":""447700900000"", 
                    ""uuid"":""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""conversation_uuid"":""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""status"":"""+type+@""",
                    ""direction"":""outbound"",
                    ""timestamp"":""2020-01-01T12:00:00.000Z""
                }";
            var callStatusWebhook = (CallStatusEvent)EventBase.ParseEvent(json);

            Assert.Equal("442079460000", callStatusWebhook.From);
            Assert.Equal("447700900000", callStatusWebhook.To);
            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", callStatusWebhook.Uuid);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", callStatusWebhook.ConversationUuid);
            Assert.Equal(type, callStatusWebhook.Status);
            Assert.Equal(Direction.outbound, callStatusWebhook.Direction);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (callStatusWebhook.TimeStamp));
        }
        
        [Theory]
        [InlineData("human")]
        [InlineData("machine")]
        public void TestHumanMachine(string type)
        {
            var json = @"
                {
                    ""from"":""442079460000"", 
                    ""to"":""447700900000"", 
                    ""call_uuid"":""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""conversation_uuid"":""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""status"":""" + type+@""",
                    ""direction"":""outbound"",
                    ""timestamp"":""2020-01-01T12:00:00.000Z""
                }";
            var humanMachineWebhook = (HumanMachine)EventBase.ParseEvent(json);

            Assert.Equal("442079460000", humanMachineWebhook.From);
            Assert.Equal("447700900000", humanMachineWebhook.To);
            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", humanMachineWebhook.Uuid);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", humanMachineWebhook.ConversationUuid);
            Assert.Equal(type, humanMachineWebhook.Status);
            Assert.Equal(Direction.outbound, humanMachineWebhook.Direction);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (humanMachineWebhook.TimeStamp));
        }

        [Fact]
        public void TestAnswered()
        {
            var json = @"
                {
                    ""from"":""442079460000"", 
                    ""to"":""447700900000"", 
                    ""uuid"":""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""conversation_uuid"":""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""status"":""answered"",
                    ""direction"":""outbound"",
                    ""timestamp"":""2020-01-01T12:00:00.000Z"",
                    ""start_time"":""2020-01-01T12:00:00.000Z"",
                    ""rate"":""0.02"",
                    ""network"":""1234""
                }";
            var answeredWebhook = (Answered)EventBase.ParseEvent(json);

            Assert.Equal("442079460000", answeredWebhook.From);
            Assert.Equal("447700900000", answeredWebhook.To);
            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", answeredWebhook.Uuid);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", answeredWebhook.ConversationUuid);
            Assert.Equal("answered", answeredWebhook.Status);
            Assert.Equal(Direction.outbound, answeredWebhook.Direction);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (answeredWebhook.TimeStamp));
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (answeredWebhook.StartTime));
            Assert.Equal("1234", answeredWebhook.Network);
            Assert.Equal("0.02", answeredWebhook.Rate);
        }

        [Fact]
        public void TestCompleted()
        {
            var json = @"
                {
                    ""from"":""442079460000"", 
                    ""to"":""447700900000"", 
                    ""uuid"":""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""conversation_uuid"":""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""status"":""completed"",
                    ""direction"":""outbound"",
                    ""timestamp"":""2020-01-01T12:00:00.000Z"",
                    ""start_time"":""2020-01-01T12:00:00.000Z"",
                    ""end_time"":""2020-01-01T12:00:01.000Z"",
                    ""rate"":""0.02"",
                    ""price"":""0.03"",
                    ""network"":""1234"",
                    ""duration"":""2""
                }";
            var completedWebhook = (Completed)EventBase.ParseEvent(json);

            Assert.Equal("442079460000", completedWebhook.From);
            Assert.Equal("447700900000", completedWebhook.To);
            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", completedWebhook.Uuid);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", completedWebhook.ConversationUuid);
            Assert.Equal("completed", completedWebhook.Status);
            Assert.Equal(Direction.outbound, completedWebhook.Direction);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (completedWebhook.TimeStamp));
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (completedWebhook.StartTime));
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:01.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (completedWebhook.EndTime));
            Assert.Equal("1234", completedWebhook.Network);
            Assert.Equal("0.02", completedWebhook.Rate);
            Assert.Equal("0.03", completedWebhook.Price);
            Assert.Equal("2", completedWebhook.Duration);
        }

        [Fact]
        public void TestRecord()
        {
            var json = @"
                {
                    ""timestamp"":""2020-01-01T12:00:00.000Z"",
                    ""start_time"":""2020-01-01T12:00:00.000Z"",
                    ""end_time"":""2020-01-01T12:00:01.000Z"",
                    ""recording_url"":""https://api.nexmo.com/v1/files/bbbbbbbb-aaaa-cccc-dddd-0123456789ab"",
                    ""size"":12222,
                    ""recording_uuid"":""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""conversation_uuid"":""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab""                    
                }";
            var recordWebhook = (Voice.EventWebhooks.Record)EventBase.ParseEvent(json);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (recordWebhook.TimeStamp));
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (recordWebhook.StartTime));
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:01.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (recordWebhook.EndTime));
            Assert.Equal("https://api.nexmo.com/v1/files/bbbbbbbb-aaaa-cccc-dddd-0123456789ab", recordWebhook.RecordingUrl);
            Assert.True(12222 == recordWebhook.Size);
            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", recordWebhook.Uuid);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", recordWebhook.ConversationUuid);
        }

        [Fact]
        public void TestInput()
        {
            var json = @"
                {
                    ""from"":""442079460000"", 
                    ""to"":""447700900000"", 
                    ""uuid"":""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""conversation_uuid"":""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""dtmf"":""42"",
                    ""timed_out"":""true"",
                    ""timestamp"":""2020-01-01T12:00:00.000Z""
                }";

            var inputWebhook = (Input)EventBase.ParseEvent(json);

            Assert.Equal("442079460000", inputWebhook.From);
            Assert.Equal("447700900000", inputWebhook.To);
            Assert.Equal("42", inputWebhook.Dtmf);
            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", inputWebhook.Uuid);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", inputWebhook.ConversationUuid);                   
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (inputWebhook.TimeStamp));

        }

        [Fact]
        public void TestTransfer()
        {
            var json = @"
                {
                    ""uuid"":""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""conversation_uuid_from"":""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""conversation_uuid_to"":""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""timestamp"":""2020-01-01T12:00:00.000Z""
                }";
            var transferWebhook = (Transfer)EventBase.ParseEvent(json);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", transferWebhook.ConversationUuidFrom);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", transferWebhook.ConversationUuidTo);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (transferWebhook.TimeStamp));
            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", transferWebhook.Uuid);
        }

        [Fact]
        public void TestError()
        {
            var json = @"
                {
                    ""reason"":""Syntax error in NCCO. Invalid value type or action."",                    
                    ""conversation_uuid"":""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""timestamp"":""2020-01-01T12:00:00.000Z""
                }";
            var errorWebhook = (Error)EventBase.ParseEvent(json);
            Assert.Equal("Syntax error in NCCO. Invalid value type or action.", errorWebhook.Reason);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", errorWebhook.ConversationUuid);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (errorWebhook.TimeStamp));            
        }

        [Fact]
        public void TestNotifications()
        {
            var json = @"
                {
                    ""conversation_uuid"":""CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                    ""payload"":{""bar"":""foo""},
                    ""timestamp"":""2020-01-01T12:00:00.000Z""
                }";
            var notification = JsonConvert.DeserializeObject<Notification<Foo>>(json);
            Assert.Equal("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab", notification.ConversationUuid);
            Assert.Equal("foo", notification.Payload.bar);
            Assert.Equal(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal |
                                       DateTimeStyles.AdjustToUniversal), (notification.TimeStamp)) ;
        }
        public class Foo
        {
            public string bar { get; set; }
        }
        [Fact]
        public void TestEmpty()
        {
            Assert.Null(EventBase.ParseEvent("{}"));
        }
    }
}
