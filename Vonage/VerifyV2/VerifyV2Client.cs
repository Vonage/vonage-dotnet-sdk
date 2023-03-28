using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Sms;

namespace Vonage.VerifyV2;

/// <inheritdoc />
public class VerifyV2Client : IVerifyV2Client
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    public VerifyV2Client(VonageHttpClientConfiguration configuration) =>
        this.vonageClient = new VonageHttpClient(configuration, JsonSerializer.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<StartVerificationResponse>>
        StartVerificationAsync(Result<StartSmsVerificationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<StartSmsVerificationRequest, StartVerificationResponse>(request);
}