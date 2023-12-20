using Vonage.Test.Unit.Common.Extensions;
using Vonage.Users.UpdateUser;
using Xunit;

namespace Vonage.Test.Unit.Users.UpdateUser
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            UpdateUserRequest
                .Build()
                .WithId("USR-82e028d9-5201-4f1e-8188-604b2d3471ec")
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec");
    }
}