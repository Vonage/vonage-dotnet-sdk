using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.ProactiveConnect.Items.DeleteItem;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Items.DeleteItem;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task DeleteItem()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    "/v0.1/bulk/lists/95a462d3-ed87-4aa5-9d91-098e08093b0b/items/0f3e672d-e60e-4869-9eac-fce9047532b5")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.Helper.VonageClient.ProactiveConnectClient.DeleteItemAsync(DeleteItemRequest
                .Build()
                .WithListId(new Guid("95a462d3-ed87-4aa5-9d91-098e08093b0b"))
                .WithItemId(new Guid("0f3e672d-e60e-4869-9eac-fce9047532b5"))
                .Create())
            .Should()
            .BeSuccessAsync();
    }
}