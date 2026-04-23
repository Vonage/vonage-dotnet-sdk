#region
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Reports.CancelReport;
#endregion

namespace Vonage.Reports;

/// <summary>
///     Exposes Reports API features for requesting and managing activity reports for your Vonage account.
/// </summary>
public interface IReportsClient
{
    /// <summary>
    ///     Cancels the execution of a pending or processing asynchronous report. Reports that are already completed cannot be cancelled.
    /// </summary>
    /// <param name="request">The request containing the unique identifier of the report to cancel.</param>
    /// <returns>
    ///     A <see cref="CancelReportResponse"/> with the final state of the report, or failure if the report was not found or is already in a terminal state.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CancelReportRequest.Parse(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"));
    /// var result = await client.CancelReportAsync(request);
    /// result.Match(
    ///     response => Console.WriteLine($"Report status: {response.RequestStatus}"),
    ///     failure => Console.WriteLine($"Failed: {failure.GetFailureMessage()}"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Reports">More examples in the snippets repository</seealso>
    Task<Result<CancelReportResponse>> CancelReportAsync(Result<CancelReportRequest> request);
}
