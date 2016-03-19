using NUnit.Framework;

namespace Nexmo.Api.Test.Integration
{
    [TestFixture]
    public class VoiceTest
    {
        [Test]
        public void should_call_voice()
        {
            var result = Voice.Call(new Voice.CallCommand
            {
                to = "17775551212",
                answer_url = "https://abcdefgh.ngrok.io/content/voiceDemo.xml",
                status_url = "https://abcdefgh.ngrok.io/api/voice",
                from = "15555551212",
            });
            Assert.IsNotEmpty(result.CallId);
            Assert.AreEqual("0", result.status);
            Assert.AreEqual("17775551212", result.to);
        }

        [Test]
        public void should_text_to_speech_call()
        {
            var result = Voice.TextToSpeech(new Voice.TextToSpeechCallCommand
            {
                to = "17775551212",
                from = "15555551212",
                text = "1, 2, 3 wait <break time=\"1s\"/>5 minutes <prosody rate=\"-25%\">humanoid</prosody>",
                callback = "https://abcdefgh.ngrok.io/api/tts"
            });

            Assert.IsNotEmpty(result.call_id);
            Assert.AreEqual("0", result.status);
            Assert.AreEqual("17775551212", result.to);
        }

        [Test]
        public void should_text_to_speech_capture()
        {
            var result = Voice.TextToSpeechPrompt(new Voice.TextToSpeechPromptCaptureCommand
            {
                to = "17775551212",
                from = "15555551212",
                text = "Enter some digits please",
                bye_text = "Thanks",
                callback = "https://abcdefgh.ngrok.io/api/tts"
            });

            Assert.IsNotEmpty(result.call_id);
            Assert.AreEqual("0", result.status);
            Assert.AreEqual("17775551212", result.to);
        }

        [Test]
        public void should_text_to_speech_confirm()
        {
            var result = Voice.TextToSpeechPrompt(new Voice.TextToSpeechPromptConfirmCommand
            {
                to = "17775551212",
                text = "Please enter your 6 digit pin",
                max_digits = "6",
                pin_code = "123457",
                bye_text = "Correct!",
                failed_text = "Failed, ",
                callback = "https://abcdefgh.ngrok.io/api/tts"
            });

            Assert.IsNotEmpty(result.call_id);
            Assert.AreEqual("0", result.status);
            Assert.AreEqual("17775551212", result.to);
        }
    }
}