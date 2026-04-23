#region
using System;
using Vonage.Reports.GetReport;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.GetReport;

[Trait("Category", "Request")]
[Trait("Product", "Reports")]
public class RequestTest
{
    private static readonly Guid ValidReportId = new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5");

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        GetReportRequest.Build()
            .WithReportId(ValidReportId)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/reports/06547d61-7ac0-43bb-94bd-503b24b2a3a5");
}
