#region
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Reports.LoadRecords;

/// <summary>
///     Represents a request to load records synchronously from the Reports API.
/// </summary>
[Builder]
public readonly partial struct LoadRecordsRequest : IVonageRequest
{
    private const string DateFormat = "yyyy-MM-ddTHH:mm:sszzz";

    /// <summary>
    ///     Sets the product to return records for.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithProduct(ReportProduct.Sms)
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public ReportProduct Product { get; internal init; }

    /// <summary>
    ///     Sets the account ID (API key) to search records for.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithAccountId("12aa3456")
    /// ]]></code>
    /// </example>
    [Mandatory(1)]
    public string AccountId { get; internal init; }

    /// <summary>
    ///     Sets the start of the reporting period (inclusive). Defaults to seven days ago if not provided.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithDateStart(DateTimeOffset.Parse("2024-02-01T00:00:00+00:00"))
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<DateTimeOffset> DateStart { get; internal init; }

    /// <summary>
    ///     Sets the end of the reporting period (exclusive). Defaults to the current time if not provided.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithDateEnd(DateTimeOffset.Parse("2024-02-07T14:22:08+00:00"))
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<DateTimeOffset> DateEnd { get; internal init; }

    /// <summary>
    ///     Sets the pagination cursor for retrieving the next page of results (date-based queries only).
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCursor("MTY0OTQ3ODAwMDAwMA")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> Cursor { get; internal init; }

    /// <summary>
    ///     Sets the initialization vector for encrypted cursor pagination (date-based queries only).
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithIv("8a2c4e6f-12d3-45b6-78c9-0a1b2c3d4e5f")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> Iv { get; internal init; }

    /// <summary>
    ///     Sets the UUID of the message or call to search for. Accepts a comma-separated list of up to 20 UUIDs.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> Id { get; internal init; }

    /// <summary>
    ///     Sets the direction filter. Required for SMS and MESSAGES; optional for VOICE-CALL, VOICE-FAILED, and ASR.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithDirection(RecordDirection.Outbound)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<RecordDirection> Direction { get; internal init; }

    /// <summary>
    ///     Sets the status filter. Optional for SMS, CONVERSATION-EVENT, IN-APP-VOICE, MESSAGES, VOICE-CALL, VERIFY-V2, and NUMBER-INSIGHT-V2.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithStatus("delivered")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> Status { get; internal init; }

    /// <summary>
    ///     Sets the sender ID or number filter. Optional for SMS, MESSAGES, VOICE-CALL, VOICE-FAILED, and ASR.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithFrom("447700900001")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> From { get; internal init; }

    /// <summary>
    ///     Sets the recipient phone number filter. Optional for SMS, VOICE-CALL, VOICE-FAILED, ASR, MESSAGES, VERIFY-API, and VERIFY-V2.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithTo("447700900000")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> To { get; internal init; }

    /// <summary>
    ///     Sets the ISO two-letter country code filter. Optional for SMS, VOICE-CALL, VOICE-FAILED, ASR, MESSAGES, VERIFY-API, VERIFY-V2, NUMBER-INSIGHT, NUMBER-INSIGHT-V2, and NETWORK-API-EVENT.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCountry("GB")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> Country { get; internal init; }

    /// <summary>
    ///     Sets the mobile network code filter. Optional for SMS, VOICE-CALL, VOICE-FAILED, ASR, VERIFY-API, VERIFY-V2, NUMBER-INSIGHT, NUMBER-INSIGHT-V2, and NETWORK-API-EVENT.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithNetwork("12345")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> Network { get; internal init; }

    /// <summary>
    ///     Sets the phone number for lookup. Optional for NUMBER-INSIGHT and NUMBER-INSIGHT-V2.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithNumber("447700900000")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> Number { get; internal init; }

    /// <summary>
    ///     Sets the language/locale filter. Optional for VERIFY-API and VERIFY-V2.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithLocale("en-gb")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> Locale { get; internal init; }

    /// <summary>
    ///     Includes the message content in the response. Optional for SMS and MESSAGES.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithIncludeMessage(true)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<bool> IncludeMessage { get; internal init; }

    /// <summary>
    ///     Includes the concatenated field in the report. Optional for SMS with direction outbound.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithShowConcatenated(false)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<bool> ShowConcatenated { get; internal init; }

    /// <summary>
    ///     Sets the account reference filter. Optional for SMS.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithAccountRef("customer1234")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> AccountRef { get; internal init; }

    /// <summary>
    ///     Sets the unique call identifier filter. Optional for AMD, ASR, VOICE-CALL, VOICE-FAILED, and WEBSOCKET-CALL.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCallId("dfc0c915f38ae6701d7d114cde2556b1-1")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> CallId { get; internal init; }

    /// <summary>
    ///     Sets the unique leg identifier filter. Optional for IN-APP-VOICE.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithLegId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> LegId { get; internal init; }

    /// <summary>
    ///     Sets the messaging provider filter. Optional for MESSAGES.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithProvider(MessagesProvider.WhatsApp)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<MessagesProvider> Provider { get; internal init; }

    /// <summary>
    ///     Sets the number type filter. Optional for VERIFY-API.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithNumberType("mobile")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> NumberType { get; internal init; }

    /// <summary>
    ///     Sets the verification channel filter. Optional for VERIFY-V2.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithChannel(RecordChannel.SilentAuth)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<RecordChannel> Channel { get; internal init; }

    /// <summary>
    ///     Sets the parent request ID filter. Optional for VERIFY-V2.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithParentRequestId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> ParentRequestId { get; internal init; }

    /// <summary>
    ///     Sets the request type filter. Optional for NUMBER-INSIGHT-V2 and NETWORK-API-EVENT.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithRequestType("fraud-score")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> RequestType { get; internal init; }

    /// <summary>
    ///     Sets the risk assessment level filter. Optional for NUMBER-INSIGHT-V2.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithRisk("high")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> Risk { get; internal init; }

    /// <summary>
    ///     Filters by whether the number has been ported or swapped. Optional for NUMBER-INSIGHT-V2.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSwapped(true)
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<bool> Swapped { get; internal init; }

    /// <summary>
    ///     Sets the conversation ID filter. Optional for CONVERSATION-EVENT, CONVERSATION-MESSAGE, and IN-APP-VOICE.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithConversationId("CON-aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> ConversationId { get; internal init; }

    /// <summary>
    ///     Sets the session ID filter. Optional for VIDEO-API.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSessionId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> SessionId { get; internal init; }

    /// <summary>
    ///     Sets the meeting ID filter. Optional for VIDEO-API.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithMeetingId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> MeetingId { get; internal init; }

    /// <summary>
    ///     Sets the product name filter for Network API events. Optional for NETWORK-API-EVENT.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithProductName("simswap")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> ProductName { get; internal init; }

    /// <summary>
    ///     Sets the request session ID filter for Network API events. Optional for NETWORK-API-EVENT.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithRequestSessionId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> RequestSessionId { get; internal init; }

    /// <summary>
    ///     Sets the API path filter for Network API events. Optional for NETWORK-API-EVENT.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithProductPath("/camara/sim-swap/v040/check")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> ProductPath { get; internal init; }

    /// <summary>
    ///     Sets the correlation ID filter for Network API events. Optional for NETWORK-API-EVENT.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithCorrelationId("aaaaaaaa-bbbb-cccc-dddd-0123456789ab")
    /// ]]></code>
    /// </example>
    [Optional]
    public Maybe<string> CorrelationId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, UriHelpers.BuildUri("/v2/reports/records", this.GetQueryStringParameters()))
        .Build();

    [ValidationRule]
    internal static Result<LoadRecordsRequest> VerifyAccountId(LoadRecordsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.AccountId, nameof(request.AccountId));

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>
        {
            {"product", this.Product.AsString(EnumFormat.Description)},
            {"account_id", this.AccountId},
        };
        this.DateStart.IfSome(v => parameters.Add("date_start", v.ToString(DateFormat, CultureInfo.InvariantCulture)));
        this.DateEnd.IfSome(v => parameters.Add("date_end", v.ToString(DateFormat, CultureInfo.InvariantCulture)));
        this.Cursor.IfSome(v => parameters.Add("cursor", v));
        this.Iv.IfSome(v => parameters.Add("iv", v));
        this.Id.IfSome(v => parameters.Add("id", v));
        this.Direction.IfSome(v => parameters.Add("direction", v.AsString(EnumFormat.Description)));
        this.Status.IfSome(v => parameters.Add("status", v));
        this.From.IfSome(v => parameters.Add("from", v));
        this.To.IfSome(v => parameters.Add("to", v));
        this.Country.IfSome(v => parameters.Add("country", v));
        this.Network.IfSome(v => parameters.Add("network", v));
        this.Number.IfSome(v => parameters.Add("number", v));
        this.Locale.IfSome(v => parameters.Add("locale", v));
        this.IncludeMessage.IfSome(v => parameters.Add("include_message", v.ToString().ToLowerInvariant()));
        this.ShowConcatenated.IfSome(v => parameters.Add("show_concatenated", v.ToString().ToLowerInvariant()));
        this.AccountRef.IfSome(v => parameters.Add("account_ref", v));
        this.CallId.IfSome(v => parameters.Add("call_id", v));
        this.LegId.IfSome(v => parameters.Add("leg_id", v));
        this.Provider.IfSome(v => parameters.Add("provider", v.AsString(EnumFormat.Description)));
        this.NumberType.IfSome(v => parameters.Add("number_type", v));
        this.Channel.IfSome(v => parameters.Add("channel", v.AsString(EnumFormat.Description)));
        this.ParentRequestId.IfSome(v => parameters.Add("parent_request_id", v));
        this.RequestType.IfSome(v => parameters.Add("request_type", v));
        this.Risk.IfSome(v => parameters.Add("risk", v));
        this.Swapped.IfSome(v => parameters.Add("swapped", v.ToString().ToLowerInvariant()));
        this.ConversationId.IfSome(v => parameters.Add("conversation_id", v));
        this.SessionId.IfSome(v => parameters.Add("session_id", v));
        this.MeetingId.IfSome(v => parameters.Add("meeting_id", v));
        this.ProductName.IfSome(v => parameters.Add("product_name", v));
        this.RequestSessionId.IfSome(v => parameters.Add("request_session_id", v));
        this.ProductPath.IfSome(v => parameters.Add("product_path", v));
        this.CorrelationId.IfSome(v => parameters.Add("correlation_id", v));
        return parameters;
    }
}
