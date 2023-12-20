using Newtonsoft.Json;
using Vonage.Serialization;
using Vonage.Voice.Nccos;
using Xunit;

namespace Vonage.Test
{
    public class StreamActionTest
    {
        [Fact]
        public void TestStreamUrl()
        {
            //Arrange
            var expected = "{\"action\":\"stream\",\"streamUrl\":[\"https://www.example.com/waiting.mp3\"]}";
            var action = new StreamAction {StreamUrl = new[] {"https://www.example.com/waiting.mp3"}};

            //Act
            var serialized = JsonConvert.SerializeObject(action, VonageSerialization.SerializerSettings);

            //Assert
            Assert.Equal(expected, serialized);
        }
    }
}