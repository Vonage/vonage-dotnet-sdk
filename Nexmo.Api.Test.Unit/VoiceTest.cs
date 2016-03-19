using System;
using System.IO;
using System.Text;
using Moq;
using Nexmo.Api.Request;
using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    internal class VoiceTest : MockedWebTest
    {
        [Test]
        public void should_call_voice()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"call-id\":\"ffffffff36c53a6c37ba7bcfada6ffff-1\",\"to\":\"17775551212\",\"status\":\"0\"}")));
            var postDataStream = new MemoryStream();
            _request.Setup(e => e.GetRequestStream()).Returns(postDataStream);
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var result = Voice.Call(new Voice.CallCommand
            {
                to = "17775551212",
                answer_url = "https://test.test.com/content/voiceDemo.xml",
                from = "15555551212",
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/call/json", RestUrl))),
                Times.Once);
            postDataStream.Position = 0;
            var sr = new StreamReader(postDataStream);
            var postData = sr.ReadToEnd();
            Assert.AreEqual(string.Format("to=17775551212&answer_url=https%3a%2f%2ftest.test.com%2fcontent%2fvoiceDemo.xml&from=15555551212&api_key={0}&api_secret={1}&", ApiKey, ApiSecret), postData);

            Assert.AreEqual("ffffffff36c53a6c37ba7bcfada6ffff-1", result.CallId);
            Assert.AreEqual("17775551212", result.to);
            Assert.AreEqual("0", result.status);
        }

        [Test]
        public void should_text_to_speech()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"call_id\":\"ffffffffc839189f36354f94b878ffff\",\"to\":\"17775551212\",\"status\":\"0\",\"error_text\":\"Success\"}")));
            var postDataStream = new MemoryStream();
            _request.Setup(e => e.GetRequestStream()).Returns(postDataStream);
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var result = Voice.TextToSpeech(new Voice.TextToSpeechCallCommand
            {
                to = "17775551212",
                from = "15555551212",
                text = "1, 2, 3 wait <break time=\"1s\"/>5 minutes <prosody rate=\"-25%\">humanoid</prosody>",
                callback = "https://test.test.com/api/tts"
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/tts/json", ApiUrl))),
                Times.Once);
            postDataStream.Position = 0;
            var sr = new StreamReader(postDataStream);
            var postData = sr.ReadToEnd();
            Assert.AreEqual(string.Format("to=17775551212&from=15555551212&text=1%2c+2%2c+3+wait+%3cbreak+time%3d%221s%22%2f%3e5+minutes+%3cprosody+rate%3d%22-25%25%22%3ehumanoid%3c%2fprosody%3e&callback=https%3a%2f%2ftest.test.com%2fapi%2ftts&api_key={0}&api_secret={1}&", ApiKey, ApiSecret), postData);

            Assert.AreEqual("ffffffffc839189f36354f94b878ffff", result.call_id);
            Assert.AreEqual("17775551212", result.to);
            Assert.AreEqual("0", result.status);
            Assert.AreEqual("Success", result.error_text);
        }

        [Test]
        public void should_text_to_speech_capture()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"call_id\":\"ffffffffc839189f36354f94b878ffff\",\"to\":\"17775551212\",\"status\":\"0\",\"error_text\":\"Success\"}")));
            var postDataStream = new MemoryStream();
            _request.Setup(e => e.GetRequestStream()).Returns(postDataStream);
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var result = Voice.TextToSpeechPrompt(new Voice.TextToSpeechPromptCaptureCommand
            {
                to = "17775551212",
                from = "15555551212",
                text = "Enter some digits please",
                bye_text = "Thanks",
                callback = "https://test.test.com/api/tts"
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/tts-prompt/json", ApiUrl))),
                Times.Once);
            postDataStream.Position = 0;
            var sr = new StreamReader(postDataStream);
            var postData = sr.ReadToEnd();
            Assert.AreEqual(string.Format("from=15555551212&to=17775551212&text=Enter+some+digits+please&callback=https%3a%2f%2ftest.test.com%2fapi%2ftts&bye_text=Thanks&api_key={0}&api_secret={1}&", ApiKey, ApiSecret), postData);

            Assert.AreEqual("ffffffffc839189f36354f94b878ffff", result.call_id);
            Assert.AreEqual("17775551212", result.to);
            Assert.AreEqual("0", result.status);
            Assert.AreEqual("Success", result.error_text);
        }

        [Test]
        public void should_text_to_speech_confirm()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"call_id\":\"ffffffffc839189f36354f94b878ffff\",\"to\":\"17775551212\",\"status\":\"0\",\"error_text\":\"Success\"}")));
            var postDataStream = new MemoryStream();
            _request.Setup(e => e.GetRequestStream()).Returns(postDataStream);
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var result = Voice.TextToSpeechPrompt(new Voice.TextToSpeechPromptConfirmCommand
            {
                to = "17775551212",
                text = "Please enter your 6 digit pin",
                max_digits = "6",
                pin_code = "123457",
                bye_text = "Correct!",
                failed_text = "Failed, ",
                callback = "https://test.test.com/api/tts"
            });

            _mock.Verify(h => h.CreateHttp(new Uri(
                string.Format("{0}/tts-prompt/json", ApiUrl))),
                Times.Once);
            postDataStream.Position = 0;
            var sr = new StreamReader(postDataStream);
            var postData = sr.ReadToEnd();
            Assert.AreEqual(string.Format("pin_code=123457&failed_text=Failed%2c+&to=17775551212&text=Please+enter+your+6+digit+pin&callback=https%3a%2f%2ftest.test.com%2fapi%2ftts&max_digits=6&bye_text=Correct!&api_key={0}&api_secret={1}&", ApiKey, ApiSecret), postData);

            Assert.AreEqual("ffffffffc839189f36354f94b878ffff", result.call_id);
            Assert.AreEqual("17775551212", result.to);
            Assert.AreEqual("0", result.status);
            Assert.AreEqual("Success", result.error_text);
        }
    }
}