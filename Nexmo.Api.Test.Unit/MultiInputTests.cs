using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexmo.Api.Voice.Nccos;
using Nexmo.Api.Voice.EventWebhooks;
using Newtonsoft.Json;

namespace Nexmo.Api.Test.Unit
{
    [TestClass]
    public class MultiInputTests
    {
        [TestMethod]
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
             Assert.AreEqual(expected, actual);
        }
        [TestMethod]
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
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
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
                ""digits"": ""1234"",
                ""timed_out"": false
              },
              ""uuid"": ""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
              ""conversation_uuid"": ""bbbbbbbb-cccc-dddd-eeee-0123456789ab"",
              ""timestamp"": ""2020-01-01T14:00:00.000Z""
            }";

            var serialized = JsonConvert.DeserializeObject<MultiInput>(inboundString);

            Assert.AreEqual("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", serialized.Uuid);            
            Assert.AreEqual("end_on_silence_timeout", serialized.Speech.TimeoutReason);
            Assert.AreEqual("0.9405097", serialized.Speech.SpeechResults[0].Confidence);
            Assert.AreEqual("Sales", serialized.Speech.SpeechResults[0].Text);
            Assert.AreEqual("0.5949854", serialized.Speech.SpeechResults[2].Confidence);
            Assert.AreEqual("Sale", serialized.Speech.SpeechResults[2].Text);
            Assert.AreEqual("1234", serialized.Dtmf.Digits);
            Assert.AreEqual(false, serialized.Dtmf.TimedOut);
        }
    }
}
