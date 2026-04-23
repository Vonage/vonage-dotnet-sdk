#region
using System;
using Vonage.Common.Monads;
using Vonage.Reports;
using Vonage.Reports.LoadRecords;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Reports.LoadRecords;

[Trait("Category", "Request")]
[Trait("Product", "Reports")]
public class RequestBuilderTest
{
    private static readonly DateTimeOffset ValidDate =
        new DateTimeOffset(2024, 2, 1, 0, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Create_ShouldReturnFailure_GivenAccountIdIsEmpty() =>
        LoadRecordsRequest.Build()
            .WithProduct(ReportProduct.Sms)
            .WithAccountId(string.Empty)
            .Create()
            .Should()
            .BeParsingFailure("AccountId cannot be null or whitespace.");

    [Fact]
    public void Create_ShouldSetProduct() =>
        LoadRecordsRequest.Build()
            .WithProduct(ReportProduct.VoiceCall)
            .WithAccountId("12aa3456")
            .Create()
            .Map(r => r.Product)
            .Should()
            .BeSuccess(ReportProduct.VoiceCall);

    [Fact]
    public void Create_ShouldSetAccountId() =>
        BuildBase()
            .Create()
            .Map(r => r.AccountId)
            .Should()
            .BeSuccess("12aa3456");

    [Fact]
    public void Create_ShouldHaveNoDateStart_GivenDefault() =>
        BuildBase().Create().Map(r => r.DateStart).Should().BeSuccess(Maybe<DateTimeOffset>.None);

    [Fact]
    public void Create_ShouldSetDateStart() =>
        BuildBase().WithDateStart(ValidDate).Create()
            .Map(r => r.DateStart).Should().BeSuccess(maybe => maybe.Should().BeSome(ValidDate));

    [Fact]
    public void Create_ShouldHaveNoDateEnd_GivenDefault() =>
        BuildBase().Create().Map(r => r.DateEnd).Should().BeSuccess(Maybe<DateTimeOffset>.None);

    [Fact]
    public void Create_ShouldSetDateEnd() =>
        BuildBase().WithDateEnd(ValidDate).Create()
            .Map(r => r.DateEnd).Should().BeSuccess(maybe => maybe.Should().BeSome(ValidDate));

    [Fact]
    public void Create_ShouldHaveNoCursor_GivenDefault() =>
        BuildBase().Create().Map(r => r.Cursor).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetCursor() =>
        BuildBase().WithCursor("MTY0OTQ3ODAwMDAwMA").Create()
            .Map(r => r.Cursor).Should().BeSuccess(maybe => maybe.Should().BeSome("MTY0OTQ3ODAwMDAwMA"));

    [Fact]
    public void Create_ShouldHaveNoIv_GivenDefault() =>
        BuildBase().Create().Map(r => r.Iv).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetIv() =>
        BuildBase().WithIv("8a2c4e6f-12d3-45b6-78c9-0a1b2c3d4e5f").Create()
            .Map(r => r.Iv).Should().BeSuccess(maybe => maybe.Should().BeSome("8a2c4e6f-12d3-45b6-78c9-0a1b2c3d4e5f"));

    [Fact]
    public void Create_ShouldHaveNoId_GivenDefault() =>
        BuildBase().Create().Map(r => r.Id).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetId() =>
        BuildBase().WithId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab").Create()
            .Map(r => r.Id).Should().BeSuccess(maybe => maybe.Should().BeSome("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"));

    [Fact]
    public void Create_ShouldHaveNoDirection_GivenDefault() =>
        BuildBase().Create().Map(r => r.Direction).Should().BeSuccess(Maybe<RecordDirection>.None);

    [Fact]
    public void Create_ShouldSetDirection() =>
        BuildBase().WithDirection(RecordDirection.Outbound).Create()
            .Map(r => r.Direction).Should().BeSuccess(maybe => maybe.Should().BeSome(RecordDirection.Outbound));

    [Fact]
    public void Create_ShouldHaveNoStatus_GivenDefault() =>
        BuildBase().Create().Map(r => r.Status).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetStatus() =>
        BuildBase().WithStatus("delivered").Create()
            .Map(r => r.Status).Should().BeSuccess(maybe => maybe.Should().BeSome("delivered"));

    [Fact]
    public void Create_ShouldHaveNoFrom_GivenDefault() =>
        BuildBase().Create().Map(r => r.From).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetFrom() =>
        BuildBase().WithFrom("447700900001").Create()
            .Map(r => r.From).Should().BeSuccess(maybe => maybe.Should().BeSome("447700900001"));

    [Fact]
    public void Create_ShouldHaveNoTo_GivenDefault() =>
        BuildBase().Create().Map(r => r.To).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetTo() =>
        BuildBase().WithTo("447700900000").Create()
            .Map(r => r.To).Should().BeSuccess(maybe => maybe.Should().BeSome("447700900000"));

    [Fact]
    public void Create_ShouldHaveNoCountry_GivenDefault() =>
        BuildBase().Create().Map(r => r.Country).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetCountry() =>
        BuildBase().WithCountry("GB").Create()
            .Map(r => r.Country).Should().BeSuccess(maybe => maybe.Should().BeSome("GB"));

    [Fact]
    public void Create_ShouldHaveNoNetwork_GivenDefault() =>
        BuildBase().Create().Map(r => r.Network).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetNetwork() =>
        BuildBase().WithNetwork("12345").Create()
            .Map(r => r.Network).Should().BeSuccess(maybe => maybe.Should().BeSome("12345"));

    [Fact]
    public void Create_ShouldHaveNoNumber_GivenDefault() =>
        BuildBase().Create().Map(r => r.Number).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetNumber() =>
        BuildBase().WithNumber("447700900000").Create()
            .Map(r => r.Number).Should().BeSuccess(maybe => maybe.Should().BeSome("447700900000"));

    [Fact]
    public void Create_ShouldHaveNoLocale_GivenDefault() =>
        BuildBase().Create().Map(r => r.Locale).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetLocale() =>
        BuildBase().WithLocale("en-gb").Create()
            .Map(r => r.Locale).Should().BeSuccess(maybe => maybe.Should().BeSome("en-gb"));

    [Fact]
    public void Create_ShouldHaveNoIncludeMessage_GivenDefault() =>
        BuildBase().Create().Map(r => r.IncludeMessage).Should().BeSuccess(Maybe<bool>.None);

    [Fact]
    public void Create_ShouldSetIncludeMessage() =>
        BuildBase().WithIncludeMessage(true).Create()
            .Map(r => r.IncludeMessage).Should().BeSuccess(maybe => maybe.Should().BeSome(true));

    [Fact]
    public void Create_ShouldHaveNoShowConcatenated_GivenDefault() =>
        BuildBase().Create().Map(r => r.ShowConcatenated).Should().BeSuccess(Maybe<bool>.None);

    [Fact]
    public void Create_ShouldSetShowConcatenated() =>
        BuildBase().WithShowConcatenated(false).Create()
            .Map(r => r.ShowConcatenated).Should().BeSuccess(maybe => maybe.Should().BeSome(false));

    [Fact]
    public void Create_ShouldHaveNoAccountRef_GivenDefault() =>
        BuildBase().Create().Map(r => r.AccountRef).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetAccountRef() =>
        BuildBase().WithAccountRef("customer1234").Create()
            .Map(r => r.AccountRef).Should().BeSuccess(maybe => maybe.Should().BeSome("customer1234"));

    [Fact]
    public void Create_ShouldHaveNoCallId_GivenDefault() =>
        BuildBase().Create().Map(r => r.CallId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetCallId() =>
        BuildBase().WithCallId("dfc0c915-1").Create()
            .Map(r => r.CallId).Should().BeSuccess(maybe => maybe.Should().BeSome("dfc0c915-1"));

    [Fact]
    public void Create_ShouldHaveNoLegId_GivenDefault() =>
        BuildBase().Create().Map(r => r.LegId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetLegId() =>
        BuildBase().WithLegId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab").Create()
            .Map(r => r.LegId).Should()
            .BeSuccess(maybe => maybe.Should().BeSome("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"));

    [Fact]
    public void Create_ShouldHaveNoProvider_GivenDefault() =>
        BuildBase().Create().Map(r => r.Provider).Should().BeSuccess(Maybe<MessagesProvider>.None);

    [Fact]
    public void Create_ShouldSetProvider() =>
        BuildBase().WithProvider(MessagesProvider.WhatsApp).Create()
            .Map(r => r.Provider).Should().BeSuccess(maybe => maybe.Should().BeSome(MessagesProvider.WhatsApp));

    [Fact]
    public void Create_ShouldHaveNoNumberType_GivenDefault() =>
        BuildBase().Create().Map(r => r.NumberType).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetNumberType() =>
        BuildBase().WithNumberType("mobile").Create()
            .Map(r => r.NumberType).Should().BeSuccess(maybe => maybe.Should().BeSome("mobile"));

    [Fact]
    public void Create_ShouldHaveNoChannel_GivenDefault() =>
        BuildBase().Create().Map(r => r.Channel).Should().BeSuccess(Maybe<RecordChannel>.None);

    [Fact]
    public void Create_ShouldSetChannel() =>
        BuildBase().WithChannel(RecordChannel.SilentAuth).Create()
            .Map(r => r.Channel).Should().BeSuccess(maybe => maybe.Should().BeSome(RecordChannel.SilentAuth));

    [Fact]
    public void Create_ShouldHaveNoParentRequestId_GivenDefault() =>
        BuildBase().Create().Map(r => r.ParentRequestId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetParentRequestId() =>
        BuildBase().WithParentRequestId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab").Create()
            .Map(r => r.ParentRequestId).Should()
            .BeSuccess(maybe => maybe.Should().BeSome("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"));

    [Fact]
    public void Create_ShouldHaveNoRequestType_GivenDefault() =>
        BuildBase().Create().Map(r => r.RequestType).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetRequestType() =>
        BuildBase().WithRequestType("fraud-score").Create()
            .Map(r => r.RequestType).Should().BeSuccess(maybe => maybe.Should().BeSome("fraud-score"));

    [Fact]
    public void Create_ShouldHaveNoRisk_GivenDefault() =>
        BuildBase().Create().Map(r => r.Risk).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetRisk() =>
        BuildBase().WithRisk("high").Create()
            .Map(r => r.Risk).Should().BeSuccess(maybe => maybe.Should().BeSome("high"));

    [Fact]
    public void Create_ShouldHaveNoSwapped_GivenDefault() =>
        BuildBase().Create().Map(r => r.Swapped).Should().BeSuccess(Maybe<bool>.None);

    [Fact]
    public void Create_ShouldSetSwapped() =>
        BuildBase().WithSwapped(true).Create()
            .Map(r => r.Swapped).Should().BeSuccess(maybe => maybe.Should().BeSome(true));

    [Fact]
    public void Create_ShouldHaveNoConversationId_GivenDefault() =>
        BuildBase().Create().Map(r => r.ConversationId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetConversationId() =>
        BuildBase().WithConversationId("CON-abc").Create()
            .Map(r => r.ConversationId).Should().BeSuccess(maybe => maybe.Should().BeSome("CON-abc"));

    [Fact]
    public void Create_ShouldHaveNoSessionId_GivenDefault() =>
        BuildBase().Create().Map(r => r.SessionId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetSessionId() =>
        BuildBase().WithSessionId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab").Create()
            .Map(r => r.SessionId).Should()
            .BeSuccess(maybe => maybe.Should().BeSome("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"));

    [Fact]
    public void Create_ShouldHaveNoMeetingId_GivenDefault() =>
        BuildBase().Create().Map(r => r.MeetingId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetMeetingId() =>
        BuildBase().WithMeetingId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab").Create()
            .Map(r => r.MeetingId).Should()
            .BeSuccess(maybe => maybe.Should().BeSome("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"));

    [Fact]
    public void Create_ShouldHaveNoProductName_GivenDefault() =>
        BuildBase().Create().Map(r => r.ProductName).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetProductName() =>
        BuildBase().WithProductName("simswap").Create()
            .Map(r => r.ProductName).Should().BeSuccess(maybe => maybe.Should().BeSome("simswap"));

    [Fact]
    public void Create_ShouldHaveNoRequestSessionId_GivenDefault() =>
        BuildBase().Create().Map(r => r.RequestSessionId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetRequestSessionId() =>
        BuildBase().WithRequestSessionId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab").Create()
            .Map(r => r.RequestSessionId).Should()
            .BeSuccess(maybe => maybe.Should().BeSome("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"));

    [Fact]
    public void Create_ShouldHaveNoProductPath_GivenDefault() =>
        BuildBase().Create().Map(r => r.ProductPath).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetProductPath() =>
        BuildBase().WithProductPath("/camara/sim-swap/v040/check").Create()
            .Map(r => r.ProductPath).Should().BeSuccess(maybe => maybe.Should().BeSome("/camara/sim-swap/v040/check"));

    [Fact]
    public void Create_ShouldHaveNoCorrelationId_GivenDefault() =>
        BuildBase().Create().Map(r => r.CorrelationId).Should().BeSuccess(Maybe<string>.None);

    [Fact]
    public void Create_ShouldSetCorrelationId() =>
        BuildBase().WithCorrelationId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab").Create()
            .Map(r => r.CorrelationId).Should()
            .BeSuccess(maybe => maybe.Should().BeSome("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"));

    private static IBuilderForOptional BuildBase() =>
        LoadRecordsRequest.Build().WithProduct(ReportProduct.Sms).WithAccountId("12aa3456");
}