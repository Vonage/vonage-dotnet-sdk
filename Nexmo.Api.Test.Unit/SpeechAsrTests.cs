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
    public class SpeechAsrTests
    {
        [TestMethod]
        public void TestSerializeNccoKitchenSink()
        {
            // arrage
            var expected = @"[{""speech"":{""uuid"":""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",""endOnSilence"":1,""language"":""en-US"",""context"":[""dog"",""cat""],""startTimeout"":5,""maxDuration"":30},""action"":""input""}]";
            var settings = new SpeechSettings
            {
                Uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                EndOnSilence = 1,
                Language = "en-US",
                Context = new string[] { "dog", "cat" },
                StartTimeout = 5,
                MaxDuration = 30
            };
            var inputAction = new InputAction { SpeechSettings = settings };

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
            var expected = @"[{""speech"":{},""action"":""input""}]";
            var settings = new SpeechSettings
            {                
            };
            var inputAction = new InputAction { SpeechSettings = settings };

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
              ""uuid"": ""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
              ""conversation_uuid"": ""bbbbbbbb-cccc-dddd-eeee-0123456789ab"",
              ""timestamp"": ""2020-01-01T14:00:00.000Z""
            }";

            var serialized = JsonConvert.DeserializeObject<Input>(inboundString);

            Assert.AreEqual("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", serialized.Uuid);            
            Assert.AreEqual("end_on_silence_timeout", serialized.SpeechResult.TimeoutReason);
            Assert.AreEqual("0.9405097", serialized.SpeechResult.SpeechResults[0].Confidence);
            Assert.AreEqual("Sales", serialized.SpeechResult.SpeechResults[0].Text);
            Assert.AreEqual("0.5949854", serialized.SpeechResult.SpeechResults[2].Confidence);
            Assert.AreEqual("Sale", serialized.SpeechResult.SpeechResults[2].Text);
        }
    }
}
