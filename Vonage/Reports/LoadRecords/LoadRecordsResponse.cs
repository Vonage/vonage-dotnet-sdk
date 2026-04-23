#region
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Reports.LoadRecords;

/// <summary>
///     Represents the synchronous records response, containing request metadata, pagination, and an array of
///     product-specific records.
/// </summary>
/// <param name="RequestId">Unique ID associated with this synchronous request.</param>
/// <param name="RequestStatus">The result status of the request.</param>
/// <param name="ReceivedAt">Timestamp when the request was processed by the Reports API.</param>
/// <param name="ItemsCount">The number of records returned in this page.</param>
/// <param name="Product">The product type the records cover.</param>
/// <param name="Links">HAL links for navigation and pagination.</param>
/// <param name="Cursor">Cursor for paginating results. Present only when pagination is applicable.</param>
/// <param name="Iv">Initialization vector for cursor processing. Present only when pagination is applicable.</param>
/// <param name="IdsNotFound">Comma-separated list of IDs not found, when the request was made by ID.</param>
/// <param name="Records">The product-specific records returned by this request.</param>
public record LoadRecordsResponse(
    [property: JsonPropertyName("request_id")]
    Guid RequestId,
    [property: JsonPropertyName("request_status")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<SyncRequestStatus>))]
    SyncRequestStatus RequestStatus,
    [property: JsonPropertyName("received_at")]
    DateTimeOffset ReceivedAt,
    [property: JsonPropertyName("items_count")]
    long ItemsCount,
    [property: JsonPropertyName("product")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<ReportProduct>))]
    ReportProduct Product,
    [property: JsonPropertyName("_links")] HalLinks<LoadRecordsHalLink> Links,
    [property: JsonPropertyName("cursor")]
    [property: JsonConverter(typeof(MaybeJsonConverter<string>))]
    Maybe<string> Cursor,
    [property: JsonPropertyName("iv")]
    [property: JsonConverter(typeof(MaybeJsonConverter<string>))]
    Maybe<string> Iv,
    [property: JsonPropertyName("ids_not_found")]
    [property: JsonConverter(typeof(MaybeJsonConverter<string>))]
    Maybe<string> IdsNotFound,
    [property: JsonPropertyName("records")]
    JsonElement[] Records
);

/// <summary>
///     Represents a HAL navigation link in a synchronous records response, and can convert itself into a new request for
///     fetching the linked page.
/// </summary>
/// <param name="Href">The URL of the linked resource.</param>
public record LoadRecordsHalLink(Uri Href)
{
    private const string DateFormat = "yyyy-MM-ddTHH:mm:sszzz";

    /// <summary>
    ///     Builds a <see cref="LoadRecordsRequest" /> from the query parameters encoded in this link's URL, allowing direct
    ///     navigation to the next or previous page without manually reconstructing the request.
    /// </summary>
    /// <returns>A <see cref="Result{T}" /> containing the request, or a failure if required parameters are missing or invalid.</returns>
    public Result<LoadRecordsRequest> BuildRequest()
    {
        var p = ParseQueryParameters(this.Href);
        var builder = LoadRecordsRequest.Build()
            .WithProduct(p.Product)
            .WithAccountId(p.AccountId);
        builder = p.DateStart.Match(builder.WithDateStart, () => builder);
        builder = p.DateEnd.Match(builder.WithDateEnd, () => builder);
        builder = p.Cursor.Match(builder.WithCursor, () => builder);
        builder = p.Iv.Match(builder.WithIv, () => builder);
        builder = p.Id.Match(builder.WithId, () => builder);
        builder = p.Direction.Match(builder.WithDirection, () => builder);
        builder = p.Status.Match(builder.WithStatus, () => builder);
        builder = p.From.Match(builder.WithFrom, () => builder);
        builder = p.To.Match(builder.WithTo, () => builder);
        builder = p.Country.Match(builder.WithCountry, () => builder);
        builder = p.Network.Match(builder.WithNetwork, () => builder);
        builder = p.Number.Match(builder.WithNumber, () => builder);
        builder = p.Locale.Match(builder.WithLocale, () => builder);
        builder = p.IncludeMessage.Match(builder.WithIncludeMessage, () => builder);
        builder = p.ShowConcatenated.Match(builder.WithShowConcatenated, () => builder);
        builder = p.AccountRef.Match(builder.WithAccountRef, () => builder);
        builder = p.CallId.Match(builder.WithCallId, () => builder);
        builder = p.LegId.Match(builder.WithLegId, () => builder);
        builder = p.Provider.Match(builder.WithProvider, () => builder);
        builder = p.NumberType.Match(builder.WithNumberType, () => builder);
        builder = p.Channel.Match(builder.WithChannel, () => builder);
        builder = p.ParentRequestId.Match(builder.WithParentRequestId, () => builder);
        builder = p.RequestType.Match(builder.WithRequestType, () => builder);
        builder = p.Risk.Match(builder.WithRisk, () => builder);
        builder = p.Swapped.Match(builder.WithSwapped, () => builder);
        builder = p.ConversationId.Match(builder.WithConversationId, () => builder);
        builder = p.SessionId.Match(builder.WithSessionId, () => builder);
        builder = p.MeetingId.Match(builder.WithMeetingId, () => builder);
        builder = p.ProductName.Match(builder.WithProductName, () => builder);
        builder = p.RequestSessionId.Match(builder.WithRequestSessionId, () => builder);
        builder = p.ProductPath.Match(builder.WithProductPath, () => builder);
        builder = p.CorrelationId.Match(builder.WithCorrelationId, () => builder);
        return builder.Create();
    }

    private static ParsedParameters ParseQueryParameters(Uri uri)
    {
        var q = HttpUtility.ParseQueryString(uri.Query);
        Maybe<string> Opt(string key) => q[key] ?? Maybe<string>.None;
        return new ParsedParameters(
            Enums.Parse<ReportProduct>(q["product"] ?? string.Empty, false, EnumFormat.Description),
            q["account_id"] ?? string.Empty,
            Opt("date_start").Map(v => DateTimeOffset.Parse(v, CultureInfo.InvariantCulture)),
            Opt("date_end").Map(v => DateTimeOffset.Parse(v, CultureInfo.InvariantCulture)),
            Opt("cursor"),
            Opt("iv"),
            Opt("id"),
            Opt("direction").Map(v => Enums.Parse<RecordDirection>(v, false, EnumFormat.Description)),
            Opt("status"),
            Opt("from"),
            Opt("to"),
            Opt("country"),
            Opt("network"),
            Opt("number"),
            Opt("locale"),
            Opt("include_message").Map(bool.Parse),
            Opt("show_concatenated").Map(bool.Parse),
            Opt("account_ref"),
            Opt("call_id"),
            Opt("leg_id"),
            Opt("provider").Map(v => Enums.Parse<MessagesProvider>(v, false, EnumFormat.Description)),
            Opt("number_type"),
            Opt("channel").Map(v => Enums.Parse<RecordChannel>(v, false, EnumFormat.Description)),
            Opt("parent_request_id"),
            Opt("request_type"),
            Opt("risk"),
            Opt("swapped").Map(bool.Parse),
            Opt("conversation_id"),
            Opt("session_id"),
            Opt("meeting_id"),
            Opt("product_name"),
            Opt("request_session_id"),
            Opt("product_path"),
            Opt("correlation_id")
        );
    }

    private record ParsedParameters(
        ReportProduct Product,
        string AccountId,
        Maybe<DateTimeOffset> DateStart,
        Maybe<DateTimeOffset> DateEnd,
        Maybe<string> Cursor,
        Maybe<string> Iv,
        Maybe<string> Id,
        Maybe<RecordDirection> Direction,
        Maybe<string> Status,
        Maybe<string> From,
        Maybe<string> To,
        Maybe<string> Country,
        Maybe<string> Network,
        Maybe<string> Number,
        Maybe<string> Locale,
        Maybe<bool> IncludeMessage,
        Maybe<bool> ShowConcatenated,
        Maybe<string> AccountRef,
        Maybe<string> CallId,
        Maybe<string> LegId,
        Maybe<MessagesProvider> Provider,
        Maybe<string> NumberType,
        Maybe<RecordChannel> Channel,
        Maybe<string> ParentRequestId,
        Maybe<string> RequestType,
        Maybe<string> Risk,
        Maybe<bool> Swapped,
        Maybe<string> ConversationId,
        Maybe<string> SessionId,
        Maybe<string> MeetingId,
        Maybe<string> ProductName,
        Maybe<string> RequestSessionId,
        Maybe<string> ProductPath,
        Maybe<string> CorrelationId);
}