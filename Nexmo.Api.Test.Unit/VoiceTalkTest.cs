using Nexmo.Api.Voice;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Nexmo.Api.Test.Unit
{
    [TestClass]
    public class VoiceTalkTest
    {
        [TestMethod]
        public void TestTalkLoopZero()
        {
            //Arrange
            var expected = "{\"text\":\"hello world\",\"loop\":0}";
            var request = new Call.TalkCommand() { loop = 0, text = "hello world" };
            //Act
            var serialized = JsonConvert.SerializeObject(request,
                Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            //Assert
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void TestTalkLoopEmpty()
        {
            //Arrange
            var expected = "{\"text\":\"hello world\"}";
            var request = new Call.TalkCommand() {text = "hello world" };
            //Act
            var serialized = JsonConvert.SerializeObject(request,
                Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            //Assert
            Assert.AreEqual(expected, serialized);
        }
    }
}
