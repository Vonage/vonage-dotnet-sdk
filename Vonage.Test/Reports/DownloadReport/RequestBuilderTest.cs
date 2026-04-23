#region
using System;
using Vonage.Reports.DownloadReport;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.DownloadReport;

[Trait("Category", "Request")]
[Trait("Product", "Reports")]
public class RequestBuilderTest
{
    private static readonly Guid ValidFileId = new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5");

    [Fact]
    public void Create_ShouldReturnFailure_GivenFileIdIsEmpty() =>
        DownloadReportRequest.Build()
            .WithFileId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("FileId cannot be empty.");

    [Fact]
    public void Create_ShouldSetFileId() =>
        DownloadReportRequest.Build()
            .WithFileId(ValidFileId)
            .Create()
            .Map(r => r.FileId)
            .Should()
            .BeSuccess(ValidFileId);
}
