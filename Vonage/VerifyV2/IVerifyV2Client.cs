#region
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.VerifyV2.Cancel;
using Vonage.VerifyV2.CreateTemplate;
using Vonage.VerifyV2.CreateTemplateFragment;
using Vonage.VerifyV2.DeleteTemplate;
using Vonage.VerifyV2.DeleteTemplateFragment;
using Vonage.VerifyV2.GetTemplate;
using Vonage.VerifyV2.GetTemplateFragment;
using Vonage.VerifyV2.GetTemplates;
using Vonage.VerifyV2.NextWorkflow;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.UpdateTemplate;
using Vonage.VerifyV2.UpdateTemplateFragment;
using Vonage.VerifyV2.VerifyCode;
#endregion

namespace Vonage.VerifyV2;

/// <summary>
///     Exposes VerifyV2 features.
/// </summary>
public interface IVerifyV2Client
{
    /// <summary>
    ///     Aborts the workflow if a verification request is still active.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> CancelAsync(Result<CancelRequest> request);

    /// <summary>
    ///     Move the request onto the next workflow, if available.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> NextWorkflowAsync(Result<NextWorkflowRequest> request);

    /// <summary>
    ///     Requests a verification to be sent to a user.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The response.</returns>
    Task<Result<StartVerificationResponse>> StartVerificationAsync(Result<StartVerificationRequest> request);

    /// <summary>
    ///     Allows a code to be checked against an existing Verification request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> VerifyCodeAsync(Result<VerifyCodeRequest> request);

    /// <summary>
    ///     Creates a new template.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Template>> CreateTemplateAsync(Result<CreateTemplateRequest> request);

    /// <summary>
    ///     Creates a new template fragment.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<TemplateFragment>> CreateTemplateFragmentAsync(Result<CreateTemplateFragmentRequest> request);

    /// <summary>
    ///     Deletes a template.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> DeleteTemplateAsync(Result<DeleteTemplateRequest> request);

    /// <summary>
    ///     Deletes a template fragment.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> DeleteTemplateFragmentAsync(Result<DeleteTemplateFragmentRequest> request);

    /// <summary>
    ///     Updates a template.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Template>> UpdateTemplateAsync(Result<UpdateTemplateRequest> request);

    /// <summary>
    ///     Updates a template fragment.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<TemplateFragment>> UpdateTemplateFragmentAsync(Result<UpdateTemplateFragmentRequest> request);

    /// <summary>
    ///     Retrieves a template.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Template>> GetTemplateAsync(Result<GetTemplateRequest> request);

    /// <summary>
    ///     Retrieves a template fragment.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<TemplateFragment>> GetTemplateFragmentAsync(Result<GetTemplateFragmentRequest> request);

    /// <summary>
    ///     Retrieves templates.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<GetTemplatesResponse>> GetTemplateAsync(Result<GetTemplatesRequest> request);
}