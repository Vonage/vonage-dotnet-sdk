#region
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.VerifyV2.Cancel;
using Vonage.VerifyV2.CreateTemplate;
using Vonage.VerifyV2.CreateTemplateFragment;
using Vonage.VerifyV2.DeleteTemplate;
using Vonage.VerifyV2.DeleteTemplateFragment;
using Vonage.VerifyV2.GetTemplate;
using Vonage.VerifyV2.GetTemplates;
using Vonage.VerifyV2.NextWorkflow;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.UpdateTemplate;
using Vonage.VerifyV2.VerifyCode;
#endregion

namespace Vonage.VerifyV2;

internal class VerifyV2Client : IVerifyV2Client
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    internal VerifyV2Client(VonageHttpClientConfiguration configuration) =>
        this.vonageClient = new VonageHttpClient(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<Unit>> CancelAsync(Result<CancelRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> NextWorkflowAsync(Result<NextWorkflowRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<StartVerificationResponse>> StartVerificationAsync(Result<StartVerificationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<StartVerificationRequest, StartVerificationResponse>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> VerifyCodeAsync(Result<VerifyCodeRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<Template>> CreateTemplateAsync(Result<CreateTemplateRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateTemplateRequest, Template>(request);

    /// <inheritdoc />
    public Task<Result<TemplateFragment>> CreateTemplateFragmentAsync(Result<CreateTemplateFragmentRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateTemplateFragmentRequest, TemplateFragment>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteTemplateAsync(Result<DeleteTemplateRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteTemplateFragmentAsync(Result<DeleteTemplateFragmentRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<Template>> UpdateTemplateAsync(Result<UpdateTemplateRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateTemplateRequest, Template>(request);

    /// <inheritdoc />
    public Task<Result<Template>> GetTemplateAsync(Result<GetTemplateRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetTemplateRequest, Template>(request);

    /// <inheritdoc />
    public Task<Result<GetTemplatesResponse>> GetTemplateAsync(Result<GetTemplatesRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetTemplatesRequest, GetTemplatesResponse>(request);
}