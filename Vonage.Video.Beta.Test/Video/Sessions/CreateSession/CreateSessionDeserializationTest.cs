using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using FluentAssertions;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.CreateSession
{
    public class CreateSessionDeserializationTest
    {
        private readonly JsonSerializer serializer;

        public CreateSessionDeserializationTest()
        {
            this.serializer = new JsonSerializer();
        }

        [Fact]
        public void ShouldDeserialize()
        {
            var response = this.serializer.DeserializeObject<CreateSessionResponse[]>(this.GetResponseJson());
            response.IsSuccess.Should().BeTrue();
            var content = response.GetSuccessUnsafe();
            content.Length.Should().Be(1);
            content[0].SessionId.Should()
                .Be(
                    "2_MX5hOThlMTJjYS1mM2U1LTRkZjgtYmM2Ni1mZDRiNWYzMGI5ZTl-fjE2NzI3MzY4NzgxNjJ-bi9OeFVLbkNaVjBUUnpVSmxjbURqQ3J4flB-fg");
        }

        [Fact]
        public void ShouldDeserialize_GivenEmptyArray()
        {
            var response = this.serializer.DeserializeObject<CreateSessionResponse[]>(this.GetResponseJson());
            response.IsSuccess.Should().BeTrue();
            response.GetSuccessUnsafe().Should().BeEmpty();
        }

        private string GetResponseJson([CallerMemberName] string name = null)
        {
            var folder = this.GetType().Namespace?
                .Replace("Vonage.Video.Beta.Test.", string.Empty)
                .Replace('.', '/');
            var test = $"{folder}/Data/{name}-response.json";
            return File.Exists(test)
                ? Regex.Replace(File.ReadAllText(test), "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1")
                : string.Empty;
        }
    }
}