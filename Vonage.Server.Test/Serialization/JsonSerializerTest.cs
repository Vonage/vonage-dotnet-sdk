using System.Net;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Xunit;

namespace Vonage.Server.Test.Serialization
{
    public class JsonSerializerTest
    {
        private const string DummyString = @"{""code"":200,""id"":10,""name"":""Hello World""}";
        private readonly JsonSerializer serializer;

        public JsonSerializerTest() => this.serializer = JsonSerializerBuilder.Build();

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
        [InlineData("{}", 0, null, new HttpStatusCode())]
        [InlineData(DummyString, 10, "Hello World", HttpStatusCode.OK)]
        public void DeserializeObject_ShouldReturnSuccess_GivenDeserializationSucceeded(string value, int expectedId,
            string expectedName, HttpStatusCode expectedCode) =>
            this.serializer
                .DeserializeObject<DummyObject>(value)
                .Should()
                .BeSuccess(new DummyObject {Id = expectedId, Name = expectedName, Code = expectedCode});

        [Theory]
        [InlineData("\"custom\"", LayoutType.Custom)]
        [InlineData("\"pip\"", LayoutType.Pip)]
        [InlineData("\"bestFit\"", LayoutType.BestFit)]
        [InlineData("\"horizontalPresentation\"", LayoutType.HorizontalPresentation)]
        [InlineData("\"verticalPresentation\"", LayoutType.VerticalPresentation)]
        public void DeserializeObject_ShouldUseLayoutTypeConverter(string input, LayoutType expected) => this
            .serializer
            .DeserializeObject<LayoutType>(input)
            .Should()
            .BeSuccess(expected);

        [Fact]
        public void SerializeObject_ShouldReturnSerializedString() => this.serializer
            .SerializeObject(new DummyObject {Id = 10, Name = "Hello World", Code = HttpStatusCode.OK}).Should()
            .Be(DummyString);

        [Theory]
        [InlineData(LayoutType.Custom, "\"custom\"")]
        [InlineData(LayoutType.Pip, "\"pip\"")]
        [InlineData(LayoutType.BestFit, "\"bestFit\"")]
        [InlineData(LayoutType.HorizontalPresentation, "\"horizontalPresentation\"")]
        [InlineData(LayoutType.VerticalPresentation, "\"verticalPresentation\"")]
        public void SerializeObject_ShouldUseLayoutTypeConverter(LayoutType type, string expected) => this
            .serializer
            .SerializeObject(type)
            .Should()
            .Be(expected);

        private struct DummyObject
        {
            public HttpStatusCode Code { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}