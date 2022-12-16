using FluentAssertions;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Test.Extensions;
using Xunit;

namespace Vonage.Video.Beta.Test.Common
{
    public class JsonSerializerTest
    {
        private readonly JsonSerializer serializer;

        public JsonSerializerTest() => this.serializer = new JsonSerializer();

        [Fact]
        public void SerializeObject_ShouldReturnSerializedString() => this.serializer
            .SerializeObject(new DummyObject {Id = 10, Name = "Hello World"}).Should()
            .Be(@"{""Id"":10,""Name"":""Hello World""}");

        [Theory]
        [InlineData("Something")]
        [InlineData("")]
        public void DeserializeObject_ShouldReturnFailure_GivenDeserializationFailed(string value) =>
            this.serializer
                .DeserializeObject<DummyObject>(value)
                .Should()
                .BeFailure(failure =>
                    failure.GetFailureMessage().Should().Be($"Unable to deserialize '{value}' into 'DummyObject'."));

        [Theory]
        [InlineData("{}", 0, null)]
        [InlineData(@"{""Id"":10,""Name"":""Hello World""}", 10, "Hello World")]
        public void DeserializeObject_ShouldReturnSuccess_GivenDeserializationSucceeded(string value, int expectedId,
            string expectedName) =>
            this.serializer
                .DeserializeObject<DummyObject>(value)
                .Should()
                .BeSuccess(new DummyObject {Id = expectedId, Name = expectedName});

        private struct DummyObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}