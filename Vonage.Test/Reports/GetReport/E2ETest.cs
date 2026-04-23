#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Reports.GetReport;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Reports.GetReport;

[Trait("Category", "E2E")]
[Trait("Product", "Reports")]
public class E2ETest : E2EBase
{
    [Fact]
    public async Task GetReport()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/reports/aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(
                    nameof(ReportResponseSerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ReportsClient
            .GetReportAsync(GetReportRequest.Build()
                .WithReportId(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"))
                .Create())
            .Should()
            .BeSuccessAsync();
    }
}
