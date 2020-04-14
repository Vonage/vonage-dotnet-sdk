using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexmo.Api.Voice;
using Xunit;
using Newtonsoft.Json;

namespace Nexmo.Api.Test.Unit
{
    public class VoiceStreamTest
    {
        [Fact]
        public void TestStreamLoopZero()
        {
            //Arrange
            var expected = "{\"stream_url\":[\"https://example.com/waiting.mp3\"],\"loop\":0}";
            var request = new Call.StreamCommand() { loop = 0, stream_url = new[] { "https://example.com/waiting.mp3" } };
            //Act
            var serialized = JsonConvert.SerializeObject(request,
                Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            //Assert
            Assert.Equal(expected, serialized);
        }

        [Fact]
        public void TestStreamLoopEmpty()
        {
            //Arrange
            var expected = "{\"stream_url\":[\"https://example.com/waiting.mp3\"]}";
            var request = new Call.StreamCommand() {stream_url = new[] { "https://example.com/waiting.mp3" } };
            //Act
            var serialized = JsonConvert.SerializeObject(request,
                Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            //Assert
            Assert.Equal(expected, serialized);
        }
    }
}
