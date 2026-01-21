#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Voice.Emergency.GetNumber;
#endregion

namespace Vonage.Voice.Emergency;

/// <inheritdoc />
public class EmergencyClient : IEmergencyClient
{
    private readonly VonageHttpClient<StandardApiError> vonageClient;

    internal EmergencyClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient =
            new VonageHttpClient<StandardApiError>(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<GetNumberResponse>> GetNumberAsync(Result<GetNumberRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetNumberRequest, GetNumberResponse>(request);
}