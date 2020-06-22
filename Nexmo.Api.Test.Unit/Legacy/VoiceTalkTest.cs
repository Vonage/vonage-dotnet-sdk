using Nexmo.Api.Voice;
using Xunit;
using Newtonsoft.Json;

namespace Nexmo.Api.Test.Unit
{    
    public class VoiceTalkTest
    {
        [Fact]
        public void TestTalkLoopZero()
        {
            //Arrange
            var expected = "{\"text\":\"hello world\",\"loop\":0}";
            var request = new Call.TalkCommand() { loop = 0, text = "hello world" };
            //Act
            var serialized = JsonConvert.SerializeObject(request,
                Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            //Assert
            Assert.Equal(expected, serialized);
        }

        [Fact]
        public void TestTalkLoopEmpty()
        {
            //Arrange
            var expected = "{\"text\":\"hello world\"}";
            var request = new Call.TalkCommand() {text = "hello world" };
            //Act
            var serialized = JsonConvert.SerializeObject(request,
                Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            //Assert
            Assert.Equal(expected, serialized);
        }
    }
}
