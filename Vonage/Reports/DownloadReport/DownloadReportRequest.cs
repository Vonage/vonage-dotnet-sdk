#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Reports.DownloadReport;

/// <summary>
///     Represents a request to download the zipped archive of a completed report. The file is available for 72 hours
///     after the report reaches <see cref="ReportStatus.Success"/>. Obtain the file ID from
///     <see cref="ReportLinks.DownloadReport"/>.
/// </summary>
[Builder]
public readonly partial struct DownloadReportRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the unique identifier (UUID) of the file to download.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithFileId(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"))
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public Guid FileId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, $"/v3/media/{this.FileId}")
        .Build();

    [ValidationRule]
    internal static Result<DownloadReportRequest> VerifyFileId(DownloadReportRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.FileId, nameof(request.FileId));
}
