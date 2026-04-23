#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Reports;

/// <summary>
///     Represents the state of an asynchronous report, returned by both the get-status and cancel-report endpoints.
/// </summary>
/// <param name="RequestId">The unique identifier of the report.</param>
/// <param name="RequestStatus">The current status of the report.</param>
/// <param name="ReceiveTime">The time the report request was received.</param>
/// <param name="StartTime">The time report processing started.</param>
/// <param name="ItemsCount">The number of records in the report.</param>
/// <param name="Links">HAL links for navigating to the report and its download.</param>
/// <param name="Product">The product type the report covers.</param>
/// <param name="AccountId">The account ID (API key) the report is for.</param>
/// <param name="DateStart">The start of the reporting period.</param>
/// <param name="DateEnd">The end of the reporting period.</param>
/// <param name="IncludeSubaccounts">Whether sub-account data is included in the report.</param>
/// <param name="CallbackUrl">The webhook URL that receives a notification when the report is ready.</param>
public record ReportResponse(
    [property: JsonPropertyName("request_id")] Guid RequestId,
    [property: JsonPropertyName("request_status")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<ReportStatus>))]
    ReportStatus RequestStatus,
    [property: JsonPropertyName("receive_time")] DateTimeOffset ReceiveTime,
    [property: JsonPropertyName("start_time")] DateTimeOffset StartTime,
    [property: JsonPropertyName("items_count")] long ItemsCount,
    [property: JsonPropertyName("_links")] ReportLinks Links,
    [property: JsonPropertyName("product")]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<ReportProduct>))]
    ReportProduct Product,
    [property: JsonPropertyName("account_id")] string AccountId,
    [property: JsonPropertyName("date_start")]
    [property: JsonConverter(typeof(MaybeJsonConverter<DateTimeOffset>))]
    Maybe<DateTimeOffset> DateStart,
    [property: JsonPropertyName("date_end")]
    [property: JsonConverter(typeof(MaybeJsonConverter<DateTimeOffset>))]
    Maybe<DateTimeOffset> DateEnd,
    [property: JsonPropertyName("include_subaccounts")]
    [property: JsonConverter(typeof(MaybeJsonConverter<bool>))]
    Maybe<bool> IncludeSubaccounts,
    [property: JsonPropertyName("callback_url")]
    [property: JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    Maybe<Uri> CallbackUrl
);

/// <summary>
///     Represents the HAL links returned with a report response.
/// </summary>
/// <param name="Self">Link to the report itself.</param>
/// <param name="DownloadReport">Link to download the report file, if available.</param>
public record ReportLinks(
    [property: JsonPropertyName("self")] ReportLink Self,
    [property: JsonPropertyName("download_report")]
    [property: JsonConverter(typeof(MaybeJsonConverter<ReportLink>))]
    Maybe<ReportLink> DownloadReport
);

/// <summary>
///     Represents a single HAL link.
/// </summary>
/// <param name="Href">The URL of the link.</param>
public record ReportLink(
    [property: JsonPropertyName("href")] string Href
);
