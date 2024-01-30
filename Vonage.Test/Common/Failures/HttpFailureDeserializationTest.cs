using System.Net;
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Common.Failures;

[Trait("Category", "Unit")]
public class HttpFailureDeserializationTest
{
    private readonly SerializationTestHelper helper =
        new SerializationTestHelper(typeof(HttpFailureDeserializationTest).Namespace);

    [Theory]
    [InlineData("400", HttpStatusCode.BadRequest,
        "Invalid request. This response may indicate that data in your request data is invalid JSON. Or it may indicate that you do not pass in a session ID or you passed in an invalid stream ID.")]
    [InlineData("403", HttpStatusCode.Forbidden, "You passed in an invalid Vonage JWT token.")]
    [InlineData("404", HttpStatusCode.NotFound, "The session exists but has not had any streams added to it yet.")]
    [InlineData("408", HttpStatusCode.RequestTimeout, "You passed in an invalid stream ID.")]
    [InlineData("409", HttpStatusCode.Conflict, "You attempted to stop an archive that was not being recorded")]
    [InlineData("413", HttpStatusCode.RequestEntityTooLarge,
        "The type string exceeds the maximum length (128 bytes), or the data string exceeds the maximum size (8 kB).")]
    public void ShouldDeserializeError(string code, HttpStatusCode statusCode, string message) =>
        this.helper.Serializer
            .DeserializeObject<HttpFailure>(this.helper.GetResponseJsonForStatusCode(code))
            .Should()
            .BeSuccess(HttpFailure.From(statusCode, message, null));
}