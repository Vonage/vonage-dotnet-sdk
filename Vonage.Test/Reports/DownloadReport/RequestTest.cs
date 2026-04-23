#region
using System;
using Vonage.Reports.DownloadReport;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.DownloadReport;

[Trait("Category", "Request")]
[Trait("Product", "Reports")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        DownloadReportRequest.Build()
            .WithFileId(new Guid("06547d61-7ac0-43bb-94bd-503b24b2a3a5"))
            .Create()
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v3/media/06547d61-7ac0-43bb-94bd-503b24b2a3a5");
}
