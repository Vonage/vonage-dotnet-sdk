#region
using Vonage.Test.Common.Extensions;
using Vonage.Users.UpdateUser;
using Xunit;
#endregion

namespace Vonage.Test.Users.UpdateUser;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        UpdateUserRequest
            .Build()
            .WithId("USR-82e028d9-5201-4f1e-8188-604b2d3471ec")
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec");
}