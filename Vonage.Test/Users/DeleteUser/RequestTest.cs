#region
using Vonage.Test.Common.Extensions;
using Vonage.Users.DeleteUser;
using Xunit;
#endregion

namespace Vonage.Test.Users.DeleteUser;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        DeleteUserRequest.Parse("USR-82e028d9-5201-4f1e-8188-604b2d3471ec")
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenUserIdIsNullOrWhitespace(string value) =>
        DeleteUserRequest.Parse(value)
            .Should()
            .BeParsingFailure("UserId cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldReturnSuccess() =>
        DeleteUserRequest.Parse("USR-82e028d9-5201-4f1e-8188-604b2d3471ec")
            .Map(request => request.UserId)
            .Should()
            .BeSuccess("USR-82e028d9-5201-4f1e-8188-604b2d3471ec");
}