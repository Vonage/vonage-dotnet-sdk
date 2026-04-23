#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Reports.CancelReport;

/// <summary>
///     Represents a request to cancel the execution of a pending or processing asynchronous report.
/// </summary>
[Builder]
public readonly partial struct CancelReportRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the unique identifier (UUID) of the report to cancel.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithReportId(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"))
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public Guid ReportId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, $"/v2/reports/{this.ReportId}")
        .Build();

    [ValidationRule]
    internal static Result<CancelReportRequest> VerifyReportId(CancelReportRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ReportId, nameof(request.ReportId));
}
