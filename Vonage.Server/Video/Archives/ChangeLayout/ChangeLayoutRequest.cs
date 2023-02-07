using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.ChangeLayout;

/// <summary>
///     Represents a request to change the layout of an archive.
/// </summary>
public readonly struct ChangeLayoutRequest : IVonageRequest
{
    private ChangeLayoutRequest(Guid applicationId, Guid archiveId, ArchiveLayout layout)
    {
        this.ApplicationId = applicationId;
        this.ArchiveId = archiveId;
        this.Layout = layout;
    }

    /// <summary>
    ///     The application Id.
    /// </summary>
    public Guid ApplicationId { get; }

    /// <summary>
    ///     The archive Id.
    /// </summary>
    public Guid ArchiveId { get; }

    /// <summary>
    ///     The layout to apply of the archive.
    /// </summary>
    public ArchiveLayout Layout { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Put, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}/layout";

    /// <summary>
    ///     Parses the input into a ChangeLayoutRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="archiveId">The archive Id.</param>
    /// <param name="layout">The layout to apply on the archive.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<ChangeLayoutRequest> Parse(Guid applicationId, Guid archiveId, ArchiveLayout layout) =>
        Result<ChangeLayoutRequest>
            .FromSuccess(new ChangeLayoutRequest(applicationId, archiveId, layout))
            .Bind(VerifyApplicationId)
            .Bind(VerifyArchiveId);

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(new {this.Layout}),
            Encoding.UTF8,
            "application/json");

    private static Result<ChangeLayoutRequest> VerifyApplicationId(ChangeLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<ChangeLayoutRequest> VerifyArchiveId(ChangeLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(ArchiveId));
}