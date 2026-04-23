#region
using Vonage.Reports;
using Vonage.Reports.CreateReport;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.CreateReport;

[Trait("Category", "Request")]
[Trait("Product", "Reports")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        CreateReportRequest.Build()
            .WithProduct(ReportProduct.Sms)
            .WithAccountId("12aa3456")
            .Create()
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/reports");
}
