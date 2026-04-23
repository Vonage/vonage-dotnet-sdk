#region
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Reports.CancelReport;
using Vonage.Reports.GetReport;
using Vonage.Reports.LoadRecords;
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
    ///     A <see cref="ReportResponse"/> with the final state of the report, or failure if the report was not found or is already in a terminal state.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CancelReportRequest.Build()
    ///     .WithReportId(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"))
    ///     .Create();
    /// var result = await client.CancelReportAsync(request);
    /// result.Match(
    ///     response => Console.WriteLine($"Report status: {response.RequestStatus}"),
    ///     failure => Console.WriteLine($"Failed: {failure.GetFailureMessage()}"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Reports">More examples in the snippets repository</seealso>
    Task<Result<ReportResponse>> CancelReportAsync(Result<CancelReportRequest> request);

    /// <summary>
    ///     Retrieves the current status and details of an asynchronous report.
    /// </summary>
    /// <param name="request">The request containing the unique identifier of the report to retrieve.</param>
    /// <returns>
    ///     A <see cref="ReportResponse"/> with the report details and current status, or failure if the report was not found.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetReportRequest.Build()
    ///     .WithReportId(Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-0123456789ab"))
    ///     .Create();
    /// var result = await client.GetReportAsync(request);
    /// result.IfSuccess(response => Console.WriteLine($"Status: {response.RequestStatus}"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Reports">More examples in the snippets repository</seealso>
    Task<Result<ReportResponse>> GetReportAsync(Result<GetReportRequest> request);

    /// <summary>
    ///     Loads records synchronously and returns them immediately. Optimised for frequent retrievals of small batches (up to tens of thousands of records).
    ///     The response includes HAL navigation links (<see cref="LoadRecordsHalLink"/>) that can be converted directly into the next request via
    ///     <see cref="LoadRecordsHalLink.BuildRequest"/>, enabling cursor-based pagination without manually reconstructing the query string.
    /// </summary>
    /// <param name="request">The request containing the product, account ID, and optional filters.</param>
    /// <returns>
    ///     A <see cref="LoadRecordsResponse"/> containing the matching records and pagination links, or failure if required parameters are missing or credentials are invalid.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// // Load the first page using the builder:
    /// var firstRequest = LoadRecordsRequest.Build()
    ///     .WithProduct(ReportProduct.Sms)
    ///     .WithAccountId("12aa3456")
    ///     .WithDirection(RecordDirection.Outbound)
    ///     .Create();
    /// var firstPage = await client.LoadRecordsAsync(firstRequest);
    /// firstPage.IfSuccess(r => Console.WriteLine($"Records: {r.ItemsCount}"));
    ///
    /// // Navigate to the next page directly from the HAL link — no manual URL parsing needed:
    /// var nextPage = await client.LoadRecordsAsync(
    ///     firstPage.Bind(response => response.Links.Next.BuildRequest()));
    /// nextPage.IfSuccess(r => Console.WriteLine($"Next page records: {r.ItemsCount}"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Reports">More examples in the snippets repository</seealso>
    Task<Result<LoadRecordsResponse>> LoadRecordsAsync(Result<LoadRecordsRequest> request);
}
