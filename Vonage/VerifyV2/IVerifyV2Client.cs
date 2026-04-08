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
using Vonage.VerifyV2.GetTemplateFragments;
using Vonage.VerifyV2.GetTemplates;
using Vonage.VerifyV2.NextWorkflow;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.UpdateTemplate;
using Vonage.VerifyV2.UpdateTemplateFragment;
using Vonage.VerifyV2.VerifyCode;
#endregion

namespace Vonage.VerifyV2;

/// <summary>
///     Exposes Verify v2 API features for multi-channel user verification using PIN codes sent via SMS, Voice, Email, WhatsApp, or Silent Authentication.
/// </summary>
public interface IVerifyV2Client
{
    /// <summary>
    ///     Cancels an in-progress verification request. Cancellation is only possible 30 seconds after the request started and before the second event has taken place.
    /// </summary>
    /// <param name="request">The cancellation request containing the verification request ID to cancel.</param>
    /// <returns>
    ///     Success if the verification was cancelled, or Failure with error details if cancellation failed (e.g., request not found, already completed, or outside the cancellation window).
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CancelRequest.Parse(requestId);
    /// var result = await client.CancelAsync(request);
    /// result.Match(
    ///     success => Console.WriteLine("Verification cancelled"),
    ///     failure => Console.WriteLine($"Failed: {failure.GetFailureMessage()}"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<Unit>> CancelAsync(Result<CancelRequest> request);

    /// <summary>
    ///     Advances the verification to the next workflow step, if available. Use this to skip the current channel and try the next fallback channel immediately.
    /// </summary>
    /// <param name="request">The request containing the verification request ID to advance.</param>
    /// <returns>
    ///     Success if the workflow advanced to the next step, or Failure if no more workflows are available or the request was not found.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = NextWorkflowRequest.Parse(requestId);
    /// var result = await client.NextWorkflowAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<Unit>> NextWorkflowAsync(Result<NextWorkflowRequest> request);

    /// <summary>
    ///     Initiates a verification request to send a PIN code to a user through the specified workflow channels.
    /// </summary>
    /// <param name="request">The verification request containing the brand, workflows, locale, and other configuration options.</param>
    /// <returns>
    ///     A <see cref="StartVerificationResponse"/> containing the request ID for subsequent operations. For Silent Auth workflows, also includes a check URL.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = StartVerificationRequest.Build()
    ///     .WithBrand("MyApp")
    ///     .WithWorkflow(SmsWorkflow.Parse("447700900000"))
    ///     .WithFallbackWorkflow(VoiceWorkflow.Parse("447700900000"))
    ///     .Create();
    /// var result = await client.StartVerificationAsync(request);
    /// result.IfSuccess(response => Console.WriteLine($"Request ID: {response.RequestId}"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<StartVerificationResponse>> StartVerificationAsync(Result<StartVerificationRequest> request);

    /// <summary>
    ///     Validates a PIN code submitted by the user against an existing verification request.
    /// </summary>
    /// <param name="request">The request containing the verification request ID and the code to validate.</param>
    /// <returns>
    ///     Success if the code is correct, or Failure if the code is invalid or the maximum number of attempts (3) has been exceeded.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = VerifyCodeRequest.Build()
    ///     .WithRequestId(requestId)
    ///     .WithCode("1234")
    ///     .Create();
    /// var result = await client.VerifyCodeAsync(request);
    /// result.Match(
    ///     success => Console.WriteLine("Code verified successfully"),
    ///     failure => Console.WriteLine($"Invalid code: {failure.GetFailureMessage()}"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<Unit>> VerifyCodeAsync(Result<VerifyCodeRequest> request);

    /// <summary>
    ///     Creates a new custom verification template. Templates allow customization of the verification message text.
    /// </summary>
    /// <param name="request">The request containing the template name. Names must be unique and match the pattern ^[A-Za-z0-9_-]+$.</param>
    /// <returns>
    ///     The created <see cref="Template"/> with its assigned ID, or Failure if template creation is not enabled for your account.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CreateTemplateRequest.Build()
    ///     .WithName("my-custom-template")
    ///     .Create();
    /// var result = await client.CreateTemplateAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<Template>> CreateTemplateAsync(Result<CreateTemplateRequest> request);

    /// <summary>
    ///     Creates a new template fragment for a specific channel and locale. Fragments define the actual message text used for verification.
    /// </summary>
    /// <param name="request">The request containing the template ID, channel (SMS or Voice), locale, and message text with placeholders.</param>
    /// <returns>
    ///     The created <see cref="TemplateFragment"/>, or Failure if the template is not found or the channel/locale combination already exists.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CreateTemplateFragmentRequest.Build()
    ///     .WithTemplateId(templateId)
    ///     .WithText("Your ${brand} code is ${code}. Valid for ${time-limit} ${time-limit-unit}.")
    ///     .WithLocale("en-us")
    ///     .WithChannel(VerificationChannel.Sms)
    ///     .Create();
    /// var result = await client.CreateTemplateFragmentAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<TemplateFragment>> CreateTemplateFragmentAsync(Result<CreateTemplateFragmentRequest> request);

    /// <summary>
    ///     Deletes a template. A template can only be deleted if it has no template fragments attached.
    /// </summary>
    /// <param name="request">The request containing the template ID to delete.</param>
    /// <returns>
    ///     Success if the template was deleted, or Failure if the template was not found or still has fragments attached.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = DeleteTemplateRequest.Parse(templateId);
    /// var result = await client.DeleteTemplateAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<Unit>> DeleteTemplateAsync(Result<DeleteTemplateRequest> request);

    /// <summary>
    ///     Deletes a template fragment from a template.
    /// </summary>
    /// <param name="request">The request containing both the template ID and template fragment ID to delete.</param>
    /// <returns>
    ///     Success if the fragment was deleted, or Failure if the template or fragment was not found.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = DeleteTemplateFragmentRequest.Build()
    ///     .WithTemplateId(templateId)
    ///     .WithTemplateFragmentId(fragmentId)
    ///     .Create();
    /// var result = await client.DeleteTemplateFragmentAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<Unit>> DeleteTemplateFragmentAsync(Result<DeleteTemplateFragmentRequest> request);

    /// <summary>
    ///     Updates a template's name or default status.
    /// </summary>
    /// <param name="request">The request containing the template ID and the properties to update.</param>
    /// <returns>
    ///     The updated <see cref="Template"/>, or Failure if the template was not found or the new name conflicts with an existing template.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = UpdateTemplateRequest.Build()
    ///     .WithId(templateId)
    ///     .WithName("new-template-name")
    ///     .SetAsDefaultTemplate()
    ///     .Create();
    /// var result = await client.UpdateTemplateAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<Template>> UpdateTemplateAsync(Result<UpdateTemplateRequest> request);

    /// <summary>
    ///     Updates the text content of a template fragment.
    /// </summary>
    /// <param name="request">The request containing the template ID, fragment ID, and new text content.</param>
    /// <returns>
    ///     The updated <see cref="TemplateFragment"/>, or Failure if the template or fragment was not found.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = UpdateTemplateFragmentRequest.Build()
    ///     .WithId(templateId)
    ///     .WithFragmentId(fragmentId)
    ///     .WithText("Updated: Your ${brand} verification code is ${code}")
    ///     .Create();
    /// var result = await client.UpdateTemplateFragmentAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<TemplateFragment>> UpdateTemplateFragmentAsync(Result<UpdateTemplateFragmentRequest> request);

    /// <summary>
    ///     Retrieves a single template by its ID.
    /// </summary>
    /// <param name="request">The request containing the template ID to retrieve.</param>
    /// <returns>
    ///     The <see cref="Template"/> if found, or Failure if the template does not exist.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetTemplateRequest.Parse(templateId);
    /// var result = await client.GetTemplateAsync(request);
    /// result.IfSuccess(template => Console.WriteLine($"Template: {template.Name}"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<Template>> GetTemplateAsync(Result<GetTemplateRequest> request);

    /// <summary>
    ///     Retrieves a single template fragment by its template ID and fragment ID.
    /// </summary>
    /// <param name="request">The request containing the template ID and fragment ID to retrieve.</param>
    /// <returns>
    ///     The <see cref="TemplateFragment"/> if found, or Failure if the template or fragment does not exist.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetTemplateFragmentRequest.Parse(templateId, fragmentId);
    /// var result = await client.GetTemplateFragmentAsync(request);
    /// result.IfSuccess(fragment => Console.WriteLine($"Fragment text: {fragment.Text}"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<TemplateFragment>> GetTemplateFragmentAsync(Result<GetTemplateFragmentRequest> request);

    /// <summary>
    ///     Retrieves a paginated list of all templates for your account.
    /// </summary>
    /// <param name="request">The request containing optional pagination parameters (page, page_size).</param>
    /// <returns>
    ///     A <see cref="GetTemplatesResponse"/> containing the templates and pagination information.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetTemplatesRequest.Build()
    ///     .WithPageSize(10)
    ///     .WithPage(1)
    ///     .Create();
    /// var result = await client.GetTemplatesAsync(request);
    /// result.IfSuccess(response => {
    ///     foreach (var template in response.Embedded.Templates)
    ///         Console.WriteLine($"Template: {template.Name}");
    /// });
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<GetTemplatesResponse>> GetTemplatesAsync(Result<GetTemplatesRequest> request);

    /// <summary>
    ///     Retrieves a paginated list of all template fragments for a specific template.
    /// </summary>
    /// <param name="request">The request containing the template ID and optional pagination parameters.</param>
    /// <returns>
    ///     A <see cref="GetTemplateFragmentsResponse"/> containing the fragments and pagination information.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetTemplateFragmentsRequest.Build()
    ///     .WithTemplateId(templateId)
    ///     .WithPageSize(10)
    ///     .Create();
    /// var result = await client.GetTemplateFragmentsAsync(request);
    /// result.IfSuccess(response => {
    ///     foreach (var fragment in response.Embedded.Fragments)
    ///         Console.WriteLine($"Fragment: {fragment.Channel} - {fragment.Locale}");
    /// });
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    Task<Result<GetTemplateFragmentsResponse>> GetTemplateFragmentsAsync(Result<GetTemplateFragmentsRequest> request);
}