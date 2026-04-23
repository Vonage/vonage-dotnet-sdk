#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Reports.CancelReport;
using Vonage.Reports.GetReport;
using Vonage.Serialization;
#endregion

namespace Vonage.Reports;

/// <inheritdoc />
internal class ReportsClient : IReportsClient
{
    private readonly VonageHttpClient<StandardApiError> vonageClient;

    /// <summary>
    ///     Creates a new Reports API client.
    /// </summary>
    /// <param name="configuration">The HTTP client configuration.</param>
    public ReportsClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient =
            new VonageHttpClient<StandardApiError>(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<ReportResponse>> CancelReportAsync(Result<CancelReportRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CancelReportRequest, ReportResponse>(request);

    /// <inheritdoc />
    public Task<Result<ReportResponse>> GetReportAsync(Result<GetReportRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetReportRequest, ReportResponse>(request);
}
