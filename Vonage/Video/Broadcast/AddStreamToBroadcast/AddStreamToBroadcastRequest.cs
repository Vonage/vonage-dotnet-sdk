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

namespace Vonage.Video.Broadcast.AddStreamToBroadcast;

/// <summary>
///     Represents a request to add a stream to a broadcast.
/// </summary>
[Builder]
public readonly partial struct AddStreamToBroadcastRequest : IVonageRequest, IHasApplicationId, IHasStreamId,
    IHasBroadcastId
{
    /// <summary>
    ///     Whether to include the stream's audio.
    /// </summary>
    [JsonPropertyOrder(1)]
    [OptionalBoolean(true, "WithDisabledAudio")]
    public bool HasAudio { get; internal init; }

    /// <summary>
    ///     Whether to include the stream's video.
    /// </summary>
    [JsonPropertyOrder(2)]
    [OptionalBoolean(true, "WithDisabledVideo")]
    public bool HasVideo { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(1)]
    public Guid BroadcastId { get; internal init; }

    /// <inheritdoc />
    [JsonPropertyName("addStream")]
    [JsonPropertyOrder(0)]
    [Mandatory(2)]
    public Guid StreamId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"),
                $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}/streams")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<AddStreamToBroadcastRequest> VerifyApplicationId(AddStreamToBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<AddStreamToBroadcastRequest> VerifyBroadcastId(AddStreamToBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.BroadcastId, nameof(request.BroadcastId));

    [ValidationRule]
    internal static Result<AddStreamToBroadcastRequest> VerifyStreamId(AddStreamToBroadcastRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(request.StreamId));
}