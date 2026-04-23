#region
using System;
using Vonage.Reports.CancelReport;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.CancelReport;

[Trait("Category", "Request")]
[Trait("Product", "Reports")]
public class RequestBuilderTest
{
    private static readonly Guid ValidReportId = new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5");

    [Fact]
    public void Create_ShouldReturnFailure_GivenReportIdIsEmpty() =>
        CancelReportRequest.Build()
            .WithReportId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("ReportId cannot be empty.");

    [Fact]
    public void Create_ShouldSetReportId() =>
        CancelReportRequest.Build()
            .WithReportId(ValidReportId)
            .Create()
            .Map(request => request.ReportId)
            .Should()
            .BeSuccess(ValidReportId);
}
