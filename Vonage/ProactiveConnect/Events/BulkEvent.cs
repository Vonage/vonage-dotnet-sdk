using System;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

#pragma warning disable CS1591

namespace Vonage.ProactiveConnect.Events;

/// <summary>
///     Represents a Proactive Connect Event.
/// </summary>
/// <param name="OccurredAt">When the event occurred.</param>
/// <param name="Type">The type of event.</param>
/// <param name="Id">The event Id.</param>
/// <param name="JobId">The job Id.</param>
/// <param name="SourceContext">The source context.</param>
/// <param name="ActionId">The action Id.</param>
/// <param name="RunId">The run Id.</param>
/// <param name="RecipientId">The recipient Id.</param>
/// <param name="Data">The underlying data.</param>
public record BulkEvent(
    DateTimeOffset OccurredAt,
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<BulkEventType>))]
    BulkEventType Type,
    Guid Id,
    Guid JobId,
    [property: JsonPropertyName("src_ctx")]
    string SourceContext,
    Guid ActionId,
    Guid RunId,
    string RecipientId,
    JsonElement Data
);

public enum BulkEventType
{
    [Description("action-call-succeeded")] ActionCallSucceeded,

    [Description("action-call-failed")] ActionCallFailed,

    [Description("action-call-info")] ActionCallInfo,

    [Description("recipient-response")] RecipientResponse,

    [Description("run-item-skipped")] RunItemSkipped,

    [Description("run-item-failed")] RunItemFailed,

    [Description("run-item-submitted")] RunItemSubmitted,

    [Description("run-items-total")] RunItemsTotal,

    [Description("run-items-ready")] RunItemsReady,

    [Description("run-items-excluded")] RunItemsExcluded,
}