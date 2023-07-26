using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Users.DeleteUser;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Users.DeleteUser
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        [Fact]
        public async Task DeleteUser()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            var result =
                await this.Helper.VonageClient.UsersClient.DeleteUserAsync(
                    DeleteUserRequest.Parse("USR-82e028d9-5201-4f1e-8188-604b2d3471ec"));
            result.Should().BeSuccess();
        }
    }
}