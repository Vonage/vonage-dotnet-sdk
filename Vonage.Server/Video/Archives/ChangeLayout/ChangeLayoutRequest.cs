using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Vonage.Server.Common;
using Vonage.Server.Common.Monads;
using Vonage.Server.Common.Validation;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Archives.ChangeLayout;

/// <summary>
///     Represents a request to change the layout of an archive.
/// </summary>
public readonly struct ChangeLayoutRequest : IVideoRequest
{
    private ChangeLayoutRequest(string applicationId, string archiveId, ArchiveLayout layout)
    {
        this.ApplicationId = applicationId;
        this.ArchiveId = archiveId;
        this.Layout = layout;
    }

    /// <summary>
    ///     The application Id.
    /// </summary>
    public string ApplicationId { get; }

    /// <summary>
    ///     The archive Id.
    /// </summary>
    public string ArchiveId { get; }

    /// <summary>
    ///     The layout to apply of the archive.
    /// </summary>
    public ArchiveLayout Layout { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Put, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        httpRequest.Content = new StringContent(new JsonSerializer().SerializeObject(new {this.Layout}), Encoding.UTF8,
            "application/json");
        return httpRequest;
    }

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}/layout";

    /// <summary>
    ///     Parses the input into a ChangeLayoutRequest.
    /// </summary>
    /// <param name="applicationId">The application Id.</param>
    /// <param name="archiveId">The archive Id.</param>
    /// <param name="layout">The layout to apply on the archive.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<ChangeLayoutRequest> Parse(string applicationId, string archiveId, ArchiveLayout layout) =>
        Result<ChangeLayoutRequest>
            .FromSuccess(new ChangeLayoutRequest(applicationId, archiveId, layout))
            .Bind(VerifyApplicationId)
            .Bind(VerifyArchiveId);

    private static Result<ChangeLayoutRequest> VerifyApplicationId(ChangeLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(ApplicationId));

    private static Result<ChangeLayoutRequest> VerifyArchiveId(ChangeLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(ArchiveId));
}