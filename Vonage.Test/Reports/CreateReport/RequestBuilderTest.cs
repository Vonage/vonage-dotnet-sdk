#region
using System;
using Vonage.Common.Monads;
using Vonage.Reports;
using Vonage.Reports.CreateReport;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.CreateReport;

[Trait("Category", "Request")]
[Trait("Product", "Reports")]
public class RequestBuilderTest
{
    private static readonly DateTimeOffset ValidDate =
        new DateTimeOffset(2024, 2, 2, 13, 50, 0, TimeSpan.Zero);

    internal static Result<CreateReportRequest> BuildRequest() =>
        CreateReportRequest.Build()
            .WithProduct(ReportProduct.Sms)
            .WithAccountId("12aa3456")
            .WithDateStart(DateTimeOffset.Parse("2024-02-02T13:50:00+00:00"))
            .WithDateEnd(DateTimeOffset.Parse("2024-02-07T14:22:08+00:00"))
            .WithIncludeSubaccounts(true)
            .WithCallbackUrl(new Uri("https://example.com/webhook"))
            .WithDirection(RecordDirection.Outbound)
            .WithStatus("delivered")
            .WithFrom("447700900001")
            .WithTo("447700900000")
            .WithCountry("PL")
            .WithNetwork("12345")
            .WithClientRef("my-personal-reference")
            .WithAccountRef("customer1234")
            .WithIncludeMessage(true)
            .WithShowConcatenated(false)
            .WithCallId("dfc0c915-1")
            .WithLegId("leg-123")
            .WithProvider(MessagesProvider.WhatsApp)
            .WithLocale("en-gb")
            .WithNumber("447700900000")
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
            .Create();

    internal static Result<CreateReportRequest> BuildRequestWithRequiredFieldsOnly() =>
        CreateReportRequest.Build()
            .WithProduct(ReportProduct.Sms)
            .WithAccountId("12aa3456")
            .Create();

    [Fact]
    public void Create_ShouldReturnFailure_GivenAccountIdIsEmpty() =>
        CreateReportRequest.Build()
            .WithProduct(ReportProduct.Sms)
            .WithAccountId(string.Empty)
            .Create()
            .Should()
            .BeParsingFailure("AccountId cannot be null or whitespace.");

    [Fact]
    public void Create_ShouldSetProduct() =>
        CreateReportRequest.Build()
            .WithProduct(ReportProduct.VoiceCall)
            .WithAccountId("12aa3456")
            .Create()
            .Map(r => r.Product)
            .Should()
            .BeSuccess(ReportProduct.VoiceCall);

    [Fact]
    public void Create_ShouldSetAccountId() =>
        BuildBase().Create().Map(r => r.AccountId).Should().BeSuccess("12aa3456");

    [Fact]
    public void Create_ShouldHaveNoDateStart_GivenDefault() =>
        BuildBase().Create().Map(r => r.DateStart).Should().BeSuccess(Maybe<DateTimeOffset>.None);

    [Fact]
    public void Create_ShouldSetDateStart() =>
        BuildBase().WithDateStart(ValidDate).Create()
            .Map(r => r.DateStart).Should().BeSuccess(m => m.Should().BeSome(ValidDate));

    [Fact]
    public void Create_ShouldHaveNoDateEnd_GivenDefault() =>
        BuildBase().Create().Map(r => r.DateEnd).Should().BeSuccess(Maybe<DateTimeOffset>.None);

    [Fact]
    public void Create_ShouldSetDateEnd() =>
        BuildBase().WithDateEnd(ValidDate).Create()
            .Map(r => r.DateEnd).Should().BeSuccess(m => m.Should().BeSome(ValidDate));

    [Fact]
    public void Create_ShouldHaveNoIncludeSubaccounts_GivenDefault() =>
        BuildBase().Create().Map(r => r.IncludeSubaccounts).Should().BeSuccess(Maybe<bool>.None);

    [Fact]
    public void Create_ShouldSetIncludeSubaccounts() =>
        BuildBase().WithIncludeSubaccounts(true).Create()
            .Map(r => r.IncludeSubaccounts).Should().BeSuccess(m => m.Should().BeSome(true));

    [Fact]
    public void Create_ShouldHaveNoCallbackUrl_GivenDefault() =>
        BuildBase().Create().Map(r => r.CallbackUrl).Should().BeSuccess(Maybe<Uri>.None);

    [Fact]
    public void Create_ShouldSetCallbackUrl() =>
        BuildBase().WithCallbackUrl(new Uri("https://example.com/webhook")).Create()
            .Map(r => r.CallbackUrl).Should().BeSuccess(m => m.Should().BeSome(new Uri("https://example.com/webhook")));

    [Fact]
    public void Create_ShouldHaveNoDirection_GivenDefault() =>
        BuildBase().Create().Map(r => r.Direction).Should().BeSuccess(Maybe<RecordDirection>.None);

    [Fact]
    public void Create_ShouldSetDirection() =>
        BuildBase().WithDirection(RecordDirection.Outbound).Create()
            .Map(r => r.Direction).Should().BeSuccess(m => m.Should().BeSome(RecordDirection.Outbound));

    [Fact]
    public void Create_ShouldHaveNoStatus_GivenDefault() =>
        BuildBase().Create().Map(r => r.Status).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetStatus() =>
        BuildBase().WithStatus("delivered").Create()
            .Map(r => r.Status).Should().BeSuccess(m => m.Should().BeSome("delivered"));

    [Fact]
    public void Create_ShouldHaveNoFrom_GivenDefault() =>
        BuildBase().Create().Map(r => r.From).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetFrom() =>
        BuildBase().WithFrom("447700900001").Create()
            .Map(r => r.From).Should().BeSuccess(m => m.Should().BeSome("447700900001"));

    [Fact]
    public void Create_ShouldHaveNoTo_GivenDefault() =>
        BuildBase().Create().Map(r => r.To).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetTo() =>
        BuildBase().WithTo("447700900000").Create()
            .Map(r => r.To).Should().BeSuccess(m => m.Should().BeSome("447700900000"));

    [Fact]
    public void Create_ShouldHaveNoCountry_GivenDefault() =>
        BuildBase().Create().Map(r => r.Country).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetCountry() =>
        BuildBase().WithCountry("PL").Create()
            .Map(r => r.Country).Should().BeSuccess(m => m.Should().BeSome("PL"));

    [Fact]
    public void Create_ShouldHaveNoNetwork_GivenDefault() =>
        BuildBase().Create().Map(r => r.Network).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetNetwork() =>
        BuildBase().WithNetwork("12345").Create()
            .Map(r => r.Network).Should().BeSuccess(m => m.Should().BeSome("12345"));

    [Fact]
    public void Create_ShouldHaveNoClientRef_GivenDefault() =>
        BuildBase().Create().Map(r => r.ClientRef).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetClientRef() =>
        BuildBase().WithClientRef("my-personal-reference").Create()
            .Map(r => r.ClientRef).Should().BeSuccess(m => m.Should().BeSome("my-personal-reference"));

    [Fact]
    public void Create_ShouldHaveNoAccountRef_GivenDefault() =>
        BuildBase().Create().Map(r => r.AccountRef).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetAccountRef() =>
        BuildBase().WithAccountRef("customer1234").Create()
            .Map(r => r.AccountRef).Should().BeSuccess(m => m.Should().BeSome("customer1234"));

    [Fact]
    public void Create_ShouldHaveNoIncludeMessage_GivenDefault() =>
        BuildBase().Create().Map(r => r.IncludeMessage).Should().BeSuccess(Maybe<bool>.None);

    [Fact]
    public void Create_ShouldSetIncludeMessage() =>
        BuildBase().WithIncludeMessage(true).Create()
            .Map(r => r.IncludeMessage).Should().BeSuccess(m => m.Should().BeSome(true));

    [Fact]
    public void Create_ShouldHaveNoShowConcatenated_GivenDefault() =>
        BuildBase().Create().Map(r => r.ShowConcatenated).Should().BeSuccess(Maybe<bool>.None);

    [Fact]
    public void Create_ShouldSetShowConcatenated() =>
        BuildBase().WithShowConcatenated(false).Create()
            .Map(r => r.ShowConcatenated).Should().BeSuccess(m => m.Should().BeSome(false));

    [Fact]
    public void Create_ShouldHaveNoCallId_GivenDefault() =>
        BuildBase().Create().Map(r => r.CallId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetCallId() =>
        BuildBase().WithCallId("dfc0c915-1").Create()
            .Map(r => r.CallId).Should().BeSuccess(m => m.Should().BeSome("dfc0c915-1"));

    [Fact]
    public void Create_ShouldHaveNoLegId_GivenDefault() =>
        BuildBase().Create().Map(r => r.LegId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetLegId() =>
        BuildBase().WithLegId("leg-123").Create()
            .Map(r => r.LegId).Should().BeSuccess(m => m.Should().BeSome("leg-123"));

    [Fact]
    public void Create_ShouldHaveNoProvider_GivenDefault() =>
        BuildBase().Create().Map(r => r.Provider).Should().BeSuccess(Maybe<MessagesProvider>.None);

    [Fact]
    public void Create_ShouldSetProvider() =>
        BuildBase().WithProvider(MessagesProvider.WhatsApp).Create()
            .Map(r => r.Provider).Should().BeSuccess(m => m.Should().BeSome(MessagesProvider.WhatsApp));

    [Fact]
    public void Create_ShouldHaveNoLocale_GivenDefault() =>
        BuildBase().Create().Map(r => r.Locale).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetLocale() =>
        BuildBase().WithLocale("en-gb").Create()
            .Map(r => r.Locale).Should().BeSuccess(m => m.Should().BeSome("en-gb"));

    [Fact]
    public void Create_ShouldHaveNoNumber_GivenDefault() =>
        BuildBase().Create().Map(r => r.Number).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetNumber() =>
        BuildBase().WithNumber("447700900000").Create()
            .Map(r => r.Number).Should().BeSuccess(m => m.Should().BeSome("447700900000"));

    [Fact]
    public void Create_ShouldHaveNoNumberType_GivenDefault() =>
        BuildBase().Create().Map(r => r.NumberType).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetNumberType() =>
        BuildBase().WithNumberType("mobile").Create()
            .Map(r => r.NumberType).Should().BeSuccess(m => m.Should().BeSome("mobile"));

    [Fact]
    public void Create_ShouldHaveNoChannel_GivenDefault() =>
        BuildBase().Create().Map(r => r.Channel).Should().BeSuccess(Maybe<RecordChannel>.None);

    [Fact]
    public void Create_ShouldSetChannel() =>
        BuildBase().WithChannel(RecordChannel.SilentAuth).Create()
            .Map(r => r.Channel).Should().BeSuccess(m => m.Should().BeSome(RecordChannel.SilentAuth));

    [Fact]
    public void Create_ShouldHaveNoParentRequestId_GivenDefault() =>
        BuildBase().Create().Map(r => r.ParentRequestId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetParentRequestId() =>
        BuildBase().WithParentRequestId("parent-123").Create()
            .Map(r => r.ParentRequestId).Should().BeSuccess(m => m.Should().BeSome("parent-123"));

    [Fact]
    public void Create_ShouldHaveNoRequestType_GivenDefault() =>
        BuildBase().Create().Map(r => r.RequestType).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetRequestType() =>
        BuildBase().WithRequestType("fraud-score").Create()
            .Map(r => r.RequestType).Should().BeSuccess(m => m.Should().BeSome("fraud-score"));

    [Fact]
    public void Create_ShouldHaveNoRisk_GivenDefault() =>
        BuildBase().Create().Map(r => r.Risk).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetRisk() =>
        BuildBase().WithRisk("high").Create()
            .Map(r => r.Risk).Should().BeSuccess(m => m.Should().BeSome("high"));

    [Fact]
    public void Create_ShouldHaveNoSwapped_GivenDefault() =>
        BuildBase().Create().Map(r => r.Swapped).Should().BeSuccess(Maybe<bool>.None);

    [Fact]
    public void Create_ShouldSetSwapped() =>
        BuildBase().WithSwapped(true).Create()
            .Map(r => r.Swapped).Should().BeSuccess(m => m.Should().BeSome(true));

    [Fact]
    public void Create_ShouldHaveNoConversationId_GivenDefault() =>
        BuildBase().Create().Map(r => r.ConversationId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetConversationId() =>
        BuildBase().WithConversationId("CON-abc").Create()
            .Map(r => r.ConversationId).Should().BeSuccess(m => m.Should().BeSome("CON-abc"));

    [Fact]
    public void Create_ShouldHaveNoSessionId_GivenDefault() =>
        BuildBase().Create().Map(r => r.SessionId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetSessionId() =>
        BuildBase().WithSessionId("session-123").Create()
            .Map(r => r.SessionId).Should().BeSuccess(m => m.Should().BeSome("session-123"));

    [Fact]
    public void Create_ShouldHaveNoMeetingId_GivenDefault() =>
        BuildBase().Create().Map(r => r.MeetingId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetMeetingId() =>
        BuildBase().WithMeetingId("meeting-123").Create()
            .Map(r => r.MeetingId).Should().BeSuccess(m => m.Should().BeSome("meeting-123"));

    [Fact]
    public void Create_ShouldHaveNoProductName_GivenDefault() =>
        BuildBase().Create().Map(r => r.ProductName).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetProductName() =>
        BuildBase().WithProductName("simswap").Create()
            .Map(r => r.ProductName).Should().BeSuccess(m => m.Should().BeSome("simswap"));

    [Fact]
    public void Create_ShouldHaveNoRequestSessionId_GivenDefault() =>
        BuildBase().Create().Map(r => r.RequestSessionId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetRequestSessionId() =>
        BuildBase().WithRequestSessionId("req-session-123").Create()
            .Map(r => r.RequestSessionId).Should().BeSuccess(m => m.Should().BeSome("req-session-123"));

    [Fact]
    public void Create_ShouldHaveNoProductPath_GivenDefault() =>
        BuildBase().Create().Map(r => r.ProductPath).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetProductPath() =>
        BuildBase().WithProductPath("/camara/sim-swap").Create()
            .Map(r => r.ProductPath).Should().BeSuccess(m => m.Should().BeSome("/camara/sim-swap"));

    [Fact]
    public void Create_ShouldHaveNoCorrelationId_GivenDefault() =>
        BuildBase().Create().Map(r => r.CorrelationId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetCorrelationId() =>
        BuildBase().WithCorrelationId("corr-123").Create()
            .Map(r => r.CorrelationId).Should().BeSuccess(m => m.Should().BeSome("corr-123"));

    private static IBuilderForOptional BuildBase() =>
        CreateReportRequest.Build().WithProduct(ReportProduct.Sms).WithAccountId("12aa3456");
}
