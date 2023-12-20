using System.Net;
using System.Threading.Tasks;
using Vonage.ProactiveConnect;
using Vonage.Test.Common.Extensions;
using Vonage.Users.GetUsers;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Users.GetUsers
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetUsers()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/users")
                    .WithParam("page_size", "100")
                    .WithParam("order", "desc")
                    .WithParam("name", "Test")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.UsersClient
                .GetUsersAsync(GetUsersRequest.Build()
                    .WithName("Test")
                    .WithOrder(FetchOrder.Descending)
                    .WithPageSize(100)
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyResponse);
        }

        [Fact]
        public async Task GetUsersWithDefaultRequest()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/users")
                    .WithParam("page_size", "10")
                    .WithParam("order", "asc")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.UsersClient
                .GetUsersAsync(GetUsersRequest.Build().Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyResponse);
        }
    }
}