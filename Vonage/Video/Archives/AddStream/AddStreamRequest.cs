#region
using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.Archives.AddStream;

/// <summary>
///     Represents a request to add a stream to an archive.
/// </summary>
[Builder]
public readonly partial struct AddStreamRequest : IVonageRequest, IHasApplicationId, IHasArchiveId, IHasStreamId
{
    /// <summary>
    ///     Whether the composed archive should include the stream's audio (true, the default) or not (false).
    /// </summary>
    [OptionalBoolean(true, "DisableAudio")]
    public bool HasAudio { get; internal init; }

    /// <summary>
    ///     Whether the composed archive should include the stream's video (true, the default) or not (false).
    /// </summary>
    [OptionalBoolean(true, "DisableVideo")]
    public bool HasVideo { get; internal init; }

    /// <inheritdoc />
    [Mandatory(0, nameof(VerifyApplicationId))]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(1, nameof(VerifyArchiveId))]
    public Guid ArchiveId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(2, nameof(VerifyStreamId))]
    public Guid StreamId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}/streams")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new StringContent(
            JsonSerializerBuilder.BuildWithCamelCase()
                .SerializeObject(new {AddStream = this.StreamId, this.HasAudio, this.HasVideo}), Encoding.UTF8,
            "application/json");

    internal static Result<AddStreamRequest> VerifyApplicationId(AddStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    internal static Result<AddStreamRequest> VerifyArchiveId(AddStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(request.ArchiveId));

    internal static Result<AddStreamRequest> VerifyStreamId(AddStreamRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(request.StreamId));
}