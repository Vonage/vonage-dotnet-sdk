using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Users.DeleteUser;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Users.DeleteUser
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task DeleteUser()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.UsersClient.DeleteUserAsync(
                    DeleteUserRequest.Parse("USR-82e028d9-5201-4f1e-8188-604b2d3471ec"))
                .Should()
                .BeSuccessAsync();
        }
    }
}