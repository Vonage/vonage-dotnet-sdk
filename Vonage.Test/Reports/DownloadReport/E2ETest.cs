#region
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Reports.DownloadReport;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Reports.DownloadReport;

[Trait("Category", "E2E")]
[Trait("Product", "Reports")]
public class E2ETest : E2EBase
{
    [Fact]
    public async Task DownloadReport()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v3/media/06547d61-7ac0-43bb-94bd-503b24b2a3a5")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody("zip-content"));
        await this.Helper.VonageClient.ReportsClient
            .DownloadReportAsync(DownloadReportRequest.Build()
                .WithFileId(Guid.Parse("06547d61-7ac0-43bb-94bd-503b24b2a3a5"))
                .Create())
            .Should()
            .BeSuccessAsync(stream =>
            {
                using var reader = new StreamReader(stream);
                reader.ReadToEnd().Should().Be("zip-content");
            });
    }
}
