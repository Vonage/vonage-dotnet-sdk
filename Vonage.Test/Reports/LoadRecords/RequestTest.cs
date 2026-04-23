#region
using System;
using System.Linq;
using System.Web;
using FluentAssertions;
using Vonage.Reports;
using Vonage.Reports.LoadRecords;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.LoadRecords;

[Trait("Category", "Request")]
[Trait("Product", "Reports")]
public class RequestTest
{
    [Theory]
    [InlineData(null, "/v2/reports/records?product=SMS&account_id=12aa3456")]
    [InlineData("outbound", "/v2/reports/records?product=SMS&account_id=12aa3456&direction=outbound")]
    public void RequestUri_ShouldReturnApiEndpoint(string direction, string expectedUri)
    {
        var builder = LoadRecordsRequest.Build()
            .WithProduct(ReportProduct.Sms)
            .WithAccountId("12aa3456");
        if (direction is not null)
        {
            builder = builder.WithDirection(RecordDirection.Outbound);
        }

        builder.Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(expectedUri);
    }

    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint_WithCursor() =>
        LoadRecordsRequest.Build()
            .WithProduct(ReportProduct.Sms)
            .WithAccountId("12aa3456")
            .WithCursor("MTY0OTQ3ODAwMDAwMA")
            .Create()
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/reports/records?product=SMS&account_id=12aa3456&cursor=MTY0OTQ3ODAwMDAwMA");

    [Fact]
    public void RequestUri_ShouldContainAllParameters()
    {
        var date = new DateTimeOffset(2024, 2, 1, 0, 0, 0, TimeSpan.Zero);
        LoadRecordsRequest.Build()
            .WithProduct(ReportProduct.Sms)
            .WithAccountId("12aa3456")
            .WithDateStart(date)
            .WithDateEnd(date)
            .WithCursor("MTY0OTQ3ODAwMDAwMA")
            .WithIv("8a2c4e6f")
            .WithId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
            .WithDirection(RecordDirection.Outbound)
            .WithStatus("delivered")
            .WithFrom("447700900001")
            .WithTo("447700900000")
            .WithCountry("GB")
            .WithNetwork("12345")
            .WithNumber("447700900000")
            .WithLocale("en-gb")
            .WithIncludeMessage(true)
            .WithShowConcatenated(false)
            .WithAccountRef("customer1234")
            .WithCallId("dfc0c915-1")
            .WithLegId("leg-123")
            .WithProvider(MessagesProvider.WhatsApp)
            .WithNumberType("mobile")
            .WithChannel(RecordChannel.V2)
            .WithParentRequestId("parent-123")
            .WithRequestType("fraud-score")
            .WithRisk("high")
            .WithSwapped(true)
            .WithConversationId("CON-abc")
            .WithSessionId("session-123")
            .WithMeetingId("meeting-123")
            .WithProductName("simswap")
            .WithRequestSessionId("req-session-123")
            .WithProductPath("/camara/sim-swap")
            .WithCorrelationId("corr-123")
            .Create()
            .Map(r => r.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(uri =>
            {
                var q = HttpUtility.ParseQueryString(uri.Split('?').Last());
                q["product"].Should().Be("SMS");
                q["account_id"].Should().Be("12aa3456");
                q["date_start"].Should().NotBeNullOrEmpty();
                q["date_end"].Should().NotBeNullOrEmpty();
                q["cursor"].Should().Be("MTY0OTQ3ODAwMDAwMA");
                q["iv"].Should().Be("8a2c4e6f");
                q["id"].Should().Be("aaaaaaaa-bbbb-cccc-dddd-0123456789ab");
                q["direction"].Should().Be("outbound");
                q["status"].Should().Be("delivered");
                q["from"].Should().Be("447700900001");
                q["to"].Should().Be("447700900000");
                q["country"].Should().Be("GB");
                q["network"].Should().Be("12345");
                q["number"].Should().Be("447700900000");
                q["locale"].Should().Be("en-gb");
                q["include_message"].Should().Be("true");
                q["show_concatenated"].Should().Be("false");
                q["account_ref"].Should().Be("customer1234");
                q["call_id"].Should().Be("dfc0c915-1");
                q["leg_id"].Should().Be("leg-123");
                q["provider"].Should().Be("whatsapp");
                q["number_type"].Should().Be("mobile");
                q["channel"].Should().Be("v2");
                q["parent_request_id"].Should().Be("parent-123");
                q["request_type"].Should().Be("fraud-score");
                q["risk"].Should().Be("high");
                q["swapped"].Should().Be("true");
                q["conversation_id"].Should().Be("CON-abc");
                q["session_id"].Should().Be("session-123");
                q["meeting_id"].Should().Be("meeting-123");
                q["product_name"].Should().Be("simswap");
                q["request_session_id"].Should().Be("req-session-123");
                q["product_path"].Should().Be("/camara/sim-swap");
                q["correlation_id"].Should().Be("corr-123");
            });
    }

    [Fact]
    public void HalLink_BuildRequest_ShouldRebuildFromNextUrl()
    {
        var nextUrl = new Uri(
            "https://api.nexmo.com/v2/reports/records?product=SMS&account_id=12aa3456&direction=outbound&cursor=MTY0OTQ3ODAwMDAwMA");
        new LoadRecordsHalLink(nextUrl)
            .BuildRequest()
            .Should()
            .BeSuccess(request =>
            {
                request.Product.Should().Be(ReportProduct.Sms);
                request.AccountId.Should().Be("12aa3456");
                request.Direction.Should().BeSome(RecordDirection.Outbound);
                request.Cursor.Should().BeSome("MTY0OTQ3ODAwMDAwMA");
            });
    }

    [Fact]
    public void HalLink_BuildRequest_ShouldFail_GivenAccountIdMissing()
    {
        var nextUrl = new Uri(
            "https://api.nexmo.com/v2/reports/records?product=SMS&direction=outbound&cursor=MTY0OTQ3ODAwMDAwMA");
        new LoadRecordsHalLink(nextUrl)
            .BuildRequest()
            .Should()
            .BeParsingFailure("AccountId cannot be null or whitespace.");
    }
}