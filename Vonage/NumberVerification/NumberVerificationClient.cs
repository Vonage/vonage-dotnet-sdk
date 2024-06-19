using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.NumberVerification.Authenticate;
using Vonage.NumberVerification.Verify;
using Vonage.Serialization;

namespace Vonage.NumberVerification;

internal class NumberVerificationClient : INumberVerificationClient
{
    private readonly VonageHttpClient vonageClient;

    internal NumberVerificationClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient = new VonageHttpClient(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<AuthenticateResponse>> AuthenticateAsync(Result<AuthenticateRequest> request) =>
        request.Map(BuildAuthorizeRequest)
            .BindAsync(this.SendAuthorizeRequest)
            .Map(BuildGetTokenRequest)
            .BindAsync(this.SendGetTokenRequest)
            .Map(BuildAuthenticateResponse);

    /// <inheritdoc />
    public async Task<Result<bool>> VerifyAsync(Result<VerifyRequest> request) =>
        await request
            .Map(BuildAuthenticationRequest)
            .BindAsync(this.AuthenticateAsync)
            .Map(BuildAuthenticationHeader)
            .Map(this.BuildClientWithAuthenticationHeader)
            .BindAsync(client => client.SendWithResponseAsync<VerifyRequest, VerifyResponse>(request))
            .Map(response => response.Verified);

    private VonageHttpClient BuildClientWithAuthenticationHeader(AuthenticationHeaderValue header) =>
        this.vonageClient.WithDifferentHeader(header);

    private static Result<AuthenticateRequest> BuildAuthenticationRequest(VerifyRequest request) =>
        request.BuildAuthenticationRequest();

    private static AuthenticationHeaderValue BuildAuthenticationHeader(AuthenticateResponse authentication) =>
        authentication.BuildAuthenticationHeader();

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