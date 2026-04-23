#region
using System;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Reports;
using Vonage.Reports.CancelReport;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.CancelReport;

[Trait("Category", "Serialization")]
[Trait("Product", "Reports")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<CancelReportResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

    private static void VerifyExpectedResponse(CancelReportResponse response)
    {
        response.RequestId.Should().Be(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"));
        response.RequestStatus.Should().Be(ReportStatus.Aborted);
        response.ReceiveTime.Should().Be(DateTimeOffset.Parse("2024-02-07T14:22:08+00:00"));
        response.StartTime.Should().Be(DateTimeOffset.Parse("2024-02-07T14:22:10+00:00"));
        response.ItemsCount.Should().Be(1523);
        response.Product.Should().Be(ReportProduct.Sms);
        response.AccountId.Should().Be("12aa3456");
        response.DateStart.Should().BeSome(DateTimeOffset.Parse("2024-02-02T13:50:00+00:00"));
        response.DateEnd.Should().BeSome(DateTimeOffset.Parse("2024-02-07T14:22:08+00:00"));
        response.IncludeSubaccounts.Should().BeSome(true);
        response.CallbackUrl.Should().BeSome(new Uri("https://example.com/webhook"));
        response.Links.Self.Href.Should().Be("https://api.nexmo.com/v2/reports/aaaaaaaa-bbbb-cccc-dddd-0123456789ab");
        response.Links.DownloadReport.Should().BeSome(link =>
            link.Href.Should().Be("https://api.nexmo.com/v3/media/aaaaaaaa-bbbb-cccc-dddd-0123456789ab"));
    }
}
