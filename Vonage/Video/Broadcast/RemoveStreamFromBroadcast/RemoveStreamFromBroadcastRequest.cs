#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.Broadcast.RemoveStreamFromBroadcast;

/// <summary>
///     Represents a request to remove a stream from a broadcast.
/// </summary>
[Builder]
public readonly partial struct RemoveStreamFromBroadcastRequest : IVonageRequest, IHasApplicationId, IHasStreamId,
    IHasBroadcastId
{
    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(0, nameof(VerifyApplicationId))]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(1, nameof(VerifyBroadcastId))]
    public Guid BroadcastId { get; internal init; }

    /// <inheritdoc />
    [JsonPropertyName("removeStream")]
    [Mandatory(2, nameof(VerifyStreamId))]
    public Guid StreamId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}/streams";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    internal static Result<RemoveStreamFromBroadcastRequest> VerifyApplicationId(
        RemoveStreamFromBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    internal static Result<RemoveStreamFromBroadcastRequest>
        VerifyBroadcastId(RemoveStreamFromBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.BroadcastId, nameof(request.BroadcastId));

    internal static Result<RemoveStreamFromBroadcastRequest> VerifyStreamId(RemoveStreamFromBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(request.StreamId));
}