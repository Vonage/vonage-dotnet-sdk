using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vonage.Voice.Nccos;
using Vonage.Voice.EventWebhooks;
using Newtonsoft.Json;

namespace Vonage.Test.Unit
{    
    public class MultiInputTests : TestBase
    {
        [Fact]
        public void TestSerializeNccoKitchenSink()
        {
            // arrage
            var expected = @"[{""dtmf"":{""timeOut"":3,""maxDigits"":1,""submitOnHash"":true},""speech"":{""uuid"":[""aaaaaaaa-bbbb-cccc-dddd-0123456789ab""],""endOnSilence"":1,""language"":""en-US"",""context"":[""dog"",""cat""],""startTimeout"":5,""maxDuration"":30},""action"":""input""}]";
            var settings = new SpeechSettings
            {
                Uuid = new[] { "aaaaaaaa-bbbb-cccc-dddd-0123456789ab" },
                EndOnSilence = 1,
                Language = "en-US",
                Context = new string[] { "dog", "cat" },
                StartTimeout = 5,
                MaxDuration = 30
            };
            var dtmfSettings = new DtmfSettings { MaxDigits = 1, TimeOut = 3, SubmitOnHash = true };
            var inputAction = new MultiInputAction { Speech = settings, Dtmf=dtmfSettings };

            //act
            var ncco = new Ncco(inputAction);
            var actual = ncco.ToString();

            //assert
             Assert.Equal(expected, actual);
        }
        [Fact]
        public void TestSerializeNccoKitchenEmpty()
        {
            // arrage
            var expected = @"[{""dtmf"":{},""speech"":{""uuid"":[""aaaaaaaa-bbbb-cccc-dddd-0123456789ab""]},""action"":""input""}]";
            var settings = new SpeechSettings
            {
                Uuid = new[] { "aaaaaaaa-bbbb-cccc-dddd-0123456789ab" }
            };
            var inputAction = new MultiInputAction { Speech = settings };

            //act
            var ncco = new Ncco(inputAction);
            var actual = ncco.ToString();

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestWebhookSerialization()
        {
            //ARRANGE
            var inboundString = @"{
              ""speech"": {
                ""timeout_reason"": ""end_on_silence_timeout"",
                ""results"": [
                  {
                    ""confidence"": ""0.9405097"",
                    ""text"": ""Sales""
                  },
                  {
                    ""confidence"": ""0.70543784"",
                    ""text"": ""Sails""
                  },
                  {
                    ""confidence"": ""0.5949854"",
                    ""text"": ""Sale""
                  }
                ]
              },              
              ""dtmf"": {
                ""digits"": null,
                ""timed_out"": false
              },
              ""uuid"": ""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
              ""conversation_uuid"": ""bbbbbbbb-cccc-dddd-eeee-0123456789ab"",
              ""timestamp"": ""2020-01-01T14:00:00.000Z""
            }";

            var serialized = JsonConvert.DeserializeObject<MultiInput>(inboundString);

            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", serialized.Uuid);            
            Assert.Equal("end_on_silence_timeout", serialized.Speech.TimeoutReason);
            Assert.Equal("0.9405097", serialized.Speech.SpeechResults[0].Confidence);
            Assert.Equal("Sales", serialized.Speech.SpeechResults[0].Text);
            Assert.Equal("0.5949854", serialized.Speech.SpeechResults[2].Confidence);
            Assert.Equal("Sale", serialized.Speech.SpeechResults[2].Text);
            Assert.Null(serialized.Dtmf.Digits);
            Assert.False(serialized.Dtmf.TimedOut);
        }

        [Fact]
        public void TestWebhookSerializationSpeechOveridden()
        {
            //ARRANGE
            var inboundString = @"{
              ""speech"": {
                ""error"": ""Speech overridden by DTMF""
                },
              ""dtmf"": {
                ""digits"": ""1234"",
                ""timed_out"": false
              },
              ""uuid"": ""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
              ""conversation_uuid"": ""bbbbbbbb-cccc-dddd-eeee-0123456789ab"",
              ""timestamp"": ""2020-01-01T14:00:00.000Z""
            }";

            var serialized = JsonConvert.DeserializeObject<MultiInput>(inboundString);

            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", serialized.Uuid);
            Assert.Equal("Speech overridden by DTMF", serialized.Speech.Error);
            Assert.Equal("1234", serialized.Dtmf.Digits);
            Assert.False(serialized.Dtmf.TimedOut);
        }
    }
}
