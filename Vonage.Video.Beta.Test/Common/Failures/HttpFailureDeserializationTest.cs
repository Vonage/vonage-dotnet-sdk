using System.Net;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Xunit;

namespace Vonage.Video.Beta.Test.Common.Failures
{
    public class HttpFailureDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public HttpFailureDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(HttpFailureDeserializationTest).Namespace);

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