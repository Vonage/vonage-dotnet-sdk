using System.Net;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.GetStream
{
    public class GetStreamDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetStreamDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetStreamDeserializationTest).Namespace);

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetStreamResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Id.Should().Be("8b732909-0a06-46a2-8ea8-074e64d43422");
                    success.VideoType.Should().Be("camera");
                    success.Name.Should().Be("random");
                    success.LayoutClassList.Length.Should().Be(1);
                    success.LayoutClassList[0].Should().Be("full");
                });

        [Fact]
        public void ShouldDeserialize400() =>
            this.helper.Serializer
                .DeserializeObject<HttpFailure>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(HttpFailure.From(HttpStatusCode.BadRequest,
                    "Invalid request. This response may indicate that data in your request data is invalid JSON. Or it may indicate that you do not pass in a session ID or you passed in an invalid stream ID."));

        [Fact]
        public void ShouldDeserialize403() =>
            this.helper.Serializer
                .DeserializeObject<HttpFailure>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(HttpFailure.From(HttpStatusCode.Forbidden, "You passed in an invalid Vonage JWT token."));

        [Fact]
        public void ShouldDeserialize404() =>
            this.helper.Serializer
                .DeserializeObject<HttpFailure>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(HttpFailure.From(HttpStatusCode.NotFound,
                    "The session exists but has not had any streams added to it yet."));

        [Fact]
        public void ShouldDeserialize408() =>
            this.helper.Serializer
                .DeserializeObject<HttpFailure>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(HttpFailure.From(HttpStatusCode.RequestTimeout, "You passed in an invalid stream ID."));
    }
}