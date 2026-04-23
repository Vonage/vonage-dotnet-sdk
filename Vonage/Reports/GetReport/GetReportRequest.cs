#region
using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Reports.GetReport;

/// <summary>
///     Represents a request to retrieve the status and details of an asynchronous report.
/// </summary>
[Builder]
public readonly partial struct GetReportRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the unique identifier (UUID) of the report to retrieve.
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
        .Initialize(HttpMethod.Get, $"/v2/reports/{this.ReportId}")
        .Build();

    [ValidationRule]
    internal static Result<GetReportRequest> VerifyReportId(GetReportRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ReportId, nameof(request.ReportId));
}
