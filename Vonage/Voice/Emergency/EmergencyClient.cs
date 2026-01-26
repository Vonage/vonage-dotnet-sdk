#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Voice.Emergency.AssignNumber;
using Vonage.Voice.Emergency.GetAddress;
using Vonage.Voice.Emergency.GetAddresses;
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
    public Task<Result<EmergencyNumberResponse>> GetNumberAsync(Result<GetNumberRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetNumberRequest, EmergencyNumberResponse>(request);

    /// <inheritdoc />
    public Task<Result<EmergencyNumberResponse>> AssignNumberAsync(Result<AssignNumberRequest> request) =>
        this.vonageClient.SendWithResponseAsync<AssignNumberRequest, EmergencyNumberResponse>(request);

    /// <inheritdoc />
    public Task<Result<Address>> GetAddressAsync(Result<GetAddressRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetAddressRequest, Address>(request);

    /// <inheritdoc />
    public Task<Result<GetAddressesResponse>> GetAddressesAsync(Result<GetAddressesRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetAddressesRequest, GetAddressesResponse>(request);
}