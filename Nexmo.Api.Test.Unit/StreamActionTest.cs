using Xunit;
using Newtonsoft.Json;
using Nexmo.Api.Voice.Nccos;

namespace Nexmo.Api.Test.Unit
{    
    public class StreamActionTest
    {
        [Fact]
        public void TestStreamUrl()
        {
            //Arrange
            var expected = "{\"streamUrl\":[\"https://www.example.com/waiting.mp3\"],\"action\":\"stream\"}";
            var action = new StreamAction() { StreamUrl = new string[] { "https://www.example.com/waiting.mp3" } };
            //Act
            var serialized = JsonConvert.SerializeObject(action, Formatting.None,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            //Assert
            Assert.Equal(expected, serialized);
        }
    }
}
