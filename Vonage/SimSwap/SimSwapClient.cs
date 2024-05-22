using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.SimSwap.Authenticate;
using Vonage.SimSwap.Check;
using Vonage.SimSwap.GetSwapDate;

namespace Vonage.SimSwap;

internal class SimSwapClient : ISimSwapClient
{
    private readonly VonageHttpClient vonageClient;
    
    internal SimSwapClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient = new VonageHttpClient(configuration, JsonSerializerBuilder.BuildWithSnakeCase());
    
    /// <inheritdoc />
    public Task<Result<AuthenticateResponse>> AuthenticateAsync(Result<AuthenticateRequest> request) =>
        request.Map(BuildAuthorizeRequest)
            .BindAsync(this.SendAuthorizeRequest)
            .Map(BuildGetTokenRequest)
            .BindAsync(this.SendGetTokenRequest)
            .Map(BuildAuthenticateResponse);
    
    /// <inheritdoc />
    public async Task<Result<bool>> CheckAsync(Result<CheckRequest> request) =>
        await request.BindAsync(this.AuthenticateCheckRequest)
            .Map(BuildAuthenticationHeader)
            .Map(this.BuildClientWithAuthenticationHeader)
            .BindAsync(client => client.SendWithResponseAsync<CheckRequest, CheckResponse>(request))
            .Map(response => response.Swapped);
    
    /// <inheritdoc />
    public async Task<Result<DateTimeOffset>> GetSwapDateAsync(Result<GetSwapDateRequest> request)
    {
        return await request.BindAsync(this.AuthenticateGetSwapDateRequest)
            .Map(BuildAuthenticationHeader)
            .Map(this.BuildClientWithAuthenticationHeader)
            .BindAsync(client => client.SendWithResponseAsync<GetSwapDateRequest, GetSwapDateResponse>(request))
            .Map(response => response.LatestSimChange);
    }
    
    private VonageHttpClient BuildClientWithAuthenticationHeader(AuthenticationHeaderValue header) =>
        this.vonageClient.WithDifferentHeader(header);
    
    private static AuthenticationHeaderValue BuildAuthenticationHeader(AuthenticateResponse authentication) =>
        authentication.BuildAuthenticationHeader();
    
    private Task<Result<AuthenticateResponse>> AuthenticateCheckRequest(CheckRequest request) =>
        this.AuthenticateAsync(request.BuildAuthenticationRequest());
    
    private Task<Result<AuthenticateResponse>> AuthenticateGetSwapDateRequest(GetSwapDateRequest request) =>
        this.AuthenticateAsync(request.BuildAuthenticationRequest());
    
    private static AuthenticateResponse BuildAuthenticateResponse(GetTokenResponse response) =>
        new AuthenticateResponse(response.AccessToken);
    
    private Task<Result<GetTokenResponse>> SendGetTokenRequest(GetTokenRequest request) =>
        this.vonageClient.SendWithResponseAsync<GetTokenRequest, GetTokenResponse>(request);
    
    private Task<Result<AuthorizeResponse>> SendAuthorizeRequest(AuthorizeRequest request) =>
        this.vonageClient.SendWithResponseAsync<AuthorizeRequest, AuthorizeResponse>(request);
    
    private static GetTokenRequest BuildGetTokenRequest(AuthorizeResponse request) => request.BuildGetTokenRequest();
    
    private static AuthorizeRequest BuildAuthorizeRequest(AuthenticateRequest request) =>
        request.BuildAuthorizeRequest();
}