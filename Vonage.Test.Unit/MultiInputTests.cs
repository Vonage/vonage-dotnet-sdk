using Newtonsoft.Json;
using Vonage.Voice.EventWebhooks;
using Vonage.Voice.Nccos;
using Xunit;

namespace Vonage.Test.Unit
{
    public class MultiInputTests : TestBase
    {
        [Fact]
        public void TestSerializeNccoAllProperties()
        {
            // arrage
            var expected = GetExpectedJson();
            var settings = new SpeechSettings
            {
                Uuid = new[] { "aaaaaaaa-bbbb-cccc-dddd-0123456789ab" },
                EndOnSilence = 1,
                Language = "en-US",
                Context = new [] { "dog", "cat" },
                StartTimeout = 5,
                MaxDuration = 30
            };
            var dtmfSettings = new DtmfSettings { MaxDigits = 1, TimeOut = 3, SubmitOnHash = true };
            var inputAction = new MultiInputAction { Speech = settings, Dtmf = dtmfSettings };

            //act
            var ncco = new Ncco(inputAction);
            var actual = ncco.ToString();

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestSerializeNccoAllPropertiesEmpty()
        {
            // arrage
            var expected = GetExpectedJson();
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
            var inboundString = GetExpectedJson();

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
            var inboundString = GetExpectedJson();

            var serialized = JsonConvert.DeserializeObject<MultiInput>(inboundString);

            Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", serialized.Uuid);
            Assert.Equal("Speech overridden by DTMF", serialized.Speech.Error);
            Assert.Equal("1234", serialized.Dtmf.Digits);
            Assert.False(serialized.Dtmf.TimedOut);
        }
    }
}
