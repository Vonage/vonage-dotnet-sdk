using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Users.GetUser;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Users.GetUser
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetUser()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.UsersClient
                .GetUserAsync(GetUserRequest.Parse("USR-82e028d9-5201-4f1e-8188-604b2d3471ec"))
                .Should()
                .BeSuccessAsync(VerifyUser);
        }
    }
}