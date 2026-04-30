using System.Threading.Tasks;
using Vonage.Applications.CreateApplication;
using Vonage.Applications.DeleteApplication;
using Vonage.Applications.GetApplication;
using Vonage.Applications.ListApplications;
using Vonage.Applications.UpdateApplication;
using Vonage.Common.Monads;

namespace Vonage.Applications;

/// <summary>
///     Exposes Application API features for creating, retrieving, updating, and deleting Vonage applications.
/// </summary>
public interface IApplicationsClient
{
    /// <summary>
    ///     Lists all applications associated with the account.
    /// </summary>
    /// <param name="request">Optional pagination parameters for the listing.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing a <see cref="ListApplicationsResponse"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = ListApplicationsRequest.Build().WithPageSize(20).Create();
    /// var result = await client.ApplicationsNewClient.ListApplicationsAsync(request);
    /// result.IfSuccess(page =>
    /// {
    ///     foreach (var app in page.Embedded.Applications)
    ///         Console.WriteLine($"{app.Id}: {app.Name}");
    /// });
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Application">More examples in the snippets repository</seealso>
    Task<Result<ListApplicationsResponse>> ListApplicationsAsync(Result<ListApplicationsRequest> request);

    /// <summary>
    ///     Creates a new Vonage application.
    /// </summary>
    /// <param name="request">The application configuration, including name and optional capabilities.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the created <see cref="ApplicationResponse"/> (including the
    ///     private key) on success, or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CreateApplicationRequest.Build()
    ///     .WithName("My Voice App")
    ///     .WithCapabilities(new ApplicationCapabilities
    ///     {
    ///         Voice = new VoiceCapability
    ///         {
    ///             Webhooks = new VoiceWebhooks
    ///             {
    ///                 AnswerUrl = new VoiceWebhook("https://example.com/answer", WebhookMethod.Get)
    ///             }
    ///         }
    ///     })
    ///     .Create();
    /// var result = await client.ApplicationsNewClient.CreateApplicationAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Application">More examples in the snippets repository</seealso>
    Task<Result<ApplicationResponse>> CreateApplicationAsync(Result<CreateApplicationRequest> request);

    /// <summary>
    ///     Retrieves an existing application by its identifier.
    /// </summary>
    /// <param name="request">The request containing the application identifier.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the <see cref="ApplicationResponse"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetApplicationRequest.Build()
    ///     .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
    ///     .Create();
    /// var result = await client.ApplicationsNewClient.GetApplicationAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Application">More examples in the snippets repository</seealso>
    Task<Result<ApplicationResponse>> GetApplicationAsync(Result<GetApplicationRequest> request);

    /// <summary>
    ///     Updates an existing application. The name field is always required.
    /// </summary>
    /// <param name="request">The updated application configuration.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the updated <see cref="ApplicationResponse"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = UpdateApplicationRequest.Build()
    ///     .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
    ///     .WithName("Renamed Application")
    ///     .Create();
    /// var result = await client.ApplicationsNewClient.UpdateApplicationAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Application">More examples in the snippets repository</seealso>
    Task<Result<ApplicationResponse>> UpdateApplicationAsync(Result<UpdateApplicationRequest> request);

    /// <summary>
    ///     Deletes an application. This action cannot be undone.
    /// </summary>
    /// <param name="request">The request containing the application identifier to delete.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing <see cref="Common.Monads.Unit"/> on success,
    ///     or an error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = DeleteApplicationRequest.Build()
    ///     .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
    ///     .Create();
    /// var result = await client.ApplicationsNewClient.DeleteApplicationAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Application">More examples in the snippets repository</seealso>
    Task<Result<Unit>> DeleteApplicationAsync(Result<DeleteApplicationRequest> request);
}
