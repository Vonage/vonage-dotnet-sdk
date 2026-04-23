#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Reports.CancelReport;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Reports.CancelReport;

[Trait("Category", "E2E")]
[Trait("Product", "Reports")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task CancelReport()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/reports/aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ReportsClient
            .CancelReportAsync(CancelReportRequest.Build()
                .WithReportId(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"))
                .Create())
            .Should()
            .BeSuccessAsync();
    }
}
