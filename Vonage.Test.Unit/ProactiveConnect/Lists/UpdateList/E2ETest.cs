using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists.UpdateList;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.UpdateList
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task UpdateList()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists/8ef94367-3a18-47a7-b59e-e98835194dcb")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingPut())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ProactiveConnectClient.UpdateListAsync(
                    UpdateListRequest.Build()
                        .WithListId(new Guid("8ef94367-3a18-47a7-b59e-e98835194dcb"))
                        .WithName("Random name")
                        .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyList);
        }
    }
}