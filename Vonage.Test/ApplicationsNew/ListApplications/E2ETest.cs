using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.ApplicationsNew.ListApplications;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.ApplicationsNew.ListApplications;

[Trait("Category", "E2E")]
[Trait("Product", "ApplicationsNew")]
public class E2ETest : E2EBase
{
    private readonly SerializationTestHelper localSerialization =
        new SerializationTestHelper(typeof(E2ETest).Namespace, JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task ListApplications()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.localSerialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ApplicationsNewClient
            .ListApplicationsAsync(ListApplicationsRequest.Build().Create())
            .Should()
            .BeSuccessAsync(response =>
            {
                response.TotalItems.Should().Be(1);
                response.Embedded.Applications.Should().HaveCount(1);
                response.Embedded.Applications[0].Name.Should().Be("My Application");
            });
    }

    [Fact]
    public async Task ListApplicationsWithPagination()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/applications")
                .WithParam("page_size", "5")
                .WithParam("page", "2")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.localSerialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ApplicationsNewClient
            .ListApplicationsAsync(ListApplicationsRequest.Build().WithPageSize(5).WithPage(2).Create())
            .Should()
            .BeSuccessAsync();
    }
}
