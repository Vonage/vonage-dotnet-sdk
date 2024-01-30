using Vonage.Test.Common.Extensions;
using Vonage.Users.CreateUser;
using Xunit;

namespace Vonage.Test.Users.CreateUser;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        CreateUserRequest
            .Build()
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v1/users");
}