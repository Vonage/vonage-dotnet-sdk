#region
using Vonage.Test.Common.Extensions;
using Vonage.Users.CreateUser;
using Xunit;
#endregion

namespace Vonage.Test.Users.CreateUser;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        CreateUserRequest
            .Build()
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/users");
}