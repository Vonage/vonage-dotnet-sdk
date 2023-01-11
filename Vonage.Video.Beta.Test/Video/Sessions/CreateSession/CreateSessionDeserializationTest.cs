using System;
using FluentAssertions;
using Vonage.Video.Beta.Test.Common;
using Vonage.Video.Beta.Video.Sessions.CreateSession;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.CreateSession
{
    public class CreateSessionDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public CreateSessionDeserializationTest()
        {
            this.helper = new SerializationTestHelper(typeof(CreateSessionDeserializationTest).Namespace);
        }

        [Fact]
        public void ShouldDeserialize200()
        {
            const string expectedId =
                "2_MX5hOThlMTJjYS1mM2U1LTRkZjgtYmM2Ni1mZDRiNWYzMGI5ZTl-fjE2NzI3MzY4NzgxNjJ-bi9OeFVLbkNaVjBUUnpVSmxjbURqQ3J4flB-fg";
            var response =
                this.helper.Serializer.DeserializeObject<CreateSessionResponse[]>(this.helper.GetResponseJson());
            var content = response.IfFailure(_ => throw new InvalidOperationException());
            content.Length.Should().Be(1);
            content[0].SessionId.Should().Be(expectedId);
        }

        [Fact]
        public void ShouldDeserialize200_GivenEmptyArray()
        {
            var response =
                this.helper.Serializer.DeserializeObject<CreateSessionResponse[]>(this.helper.GetResponseJson());
            response.IfFailure(_ => throw new InvalidOperationException()).Should().BeEmpty();
        }
    }
}