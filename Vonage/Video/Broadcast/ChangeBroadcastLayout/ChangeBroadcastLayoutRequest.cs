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
using Vonage.Server;
#endregion

namespace Vonage.Video.Broadcast.ChangeBroadcastLayout;

/// <summary>
///     Represents a request to change a broadcast layout.
/// </summary>
[Builder]
public readonly partial struct ChangeBroadcastLayoutRequest : IVonageRequest, IHasApplicationId, IHasBroadcastId
{
    /// <summary>
    ///     The new layout to apply on the broadcast.
    /// </summary>
    [JsonIgnore]
    [Mandatory(2)]
    public Layout Layout { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(1)]
    public Guid BroadcastId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}/layout")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this.Layout), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<ChangeBroadcastLayoutRequest> VerifyApplicationId(ChangeBroadcastLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<ChangeBroadcastLayoutRequest> VerifyBroadcastId(ChangeBroadcastLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.BroadcastId, nameof(request.BroadcastId));
}