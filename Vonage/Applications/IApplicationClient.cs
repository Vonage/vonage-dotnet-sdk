#region
using System.Threading.Tasks;
using Vonage.Request;
#endregion

namespace Vonage.Applications;

/// <summary>
///     Exposes methods for managing Vonage applications. Applications contain the configuration for products such as
///     Voice, Messages, RTC, and Verify.
/// </summary>
/// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/main/SnippetSamples/Applications">
///     Code examples in the snippets repository
/// </seealso>
public interface IApplicationClient
{
    /// <summary>
    ///     Creates a new application with the specified configuration.
    /// </summary>
    /// <param name="request">
    ///     The application configuration including name, capabilities, and webhook settings.
    ///     See <see cref="CreateApplicationRequest" />.
    /// </param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>The created application including its generated ID and keys.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var request = new CreateApplicationRequest
    /// {
    ///     Name = "My Voice Application",
    ///     Capabilities = new ApplicationCapabilities
    ///     {
    ///         Voice = Voice.Build()
    ///             .WithAnswerUrl("https://example.com/webhooks/answer", WebhookHttpMethod.Post)
    ///             .WithEventUrl("https://example.com/webhooks/event", WebhookHttpMethod.Post)
    ///     }
    /// };
    /// var application = await client.ApplicationClient.CreateApplicationAsync(request);
    /// Console.WriteLine($"Application ID: {application.Id}");
    /// // Store the private key securely - it's only returned on creation
    /// Console.WriteLine($"Private Key: {application.Keys.PrivateKey}");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Application">More examples in the snippets repository</seealso>
    Task<Application> CreateApplicationAsync(CreateApplicationRequest request, Credentials creds = null);

    /// <summary>
    ///     Deletes an application. This action cannot be undone.
    /// </summary>
    /// <param name="id">The unique identifier of the application to delete.</param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>True if the application was successfully deleted.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var success = await client.ApplicationClient.DeleteApplicationAsync("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Application">More examples in the snippets repository</seealso>
    Task<bool> DeleteApplicationAsync(string id, Credentials creds = null);

    /// <summary>
    ///     Retrieves the details of an existing application.
    /// </summary>
    /// <param name="id">The unique identifier of the application to retrieve.</param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>The application details including its capabilities and webhook configuration.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var application = await client.ApplicationClient.GetApplicationAsync("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");
    /// Console.WriteLine($"Application Name: {application.Name}");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Application">More examples in the snippets repository</seealso>
    Task<Application> GetApplicationAsync(string id, Credentials creds = null);

    /// <summary>
    ///     Lists all applications associated with the account.
    /// </summary>
    /// <param name="request">Pagination parameters for the request. See <see cref="ListApplicationsRequest" />.</param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>A paginated list of applications.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var request = new ListApplicationsRequest { PageSize = 10, Page = 1 };
    /// var page = await client.ApplicationClient.ListApplicationsAsync(request);
    /// foreach (var app in page.Embedded.Applications)
    /// {
    ///     Console.WriteLine($"{app.Id}: {app.Name}");
    /// }
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Application">More examples in the snippets repository</seealso>
    Task<ApplicationPage> ListApplicationsAsync(ListApplicationsRequest request, Credentials creds = null);

    /// <summary>
    ///     Updates an existing application's configuration.
    /// </summary>
    /// <param name="id">The unique identifier of the application to update.</param>
    /// <param name="request">The updated application configuration. See <see cref="CreateApplicationRequest" />.</param>
    /// <param name="creds">Optional credentials to override the default credentials.</param>
    /// <returns>The updated application.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var request = new CreateApplicationRequest
    /// {
    ///     Name = "Updated Application Name",
    ///     Capabilities = new ApplicationCapabilities
    ///     {
    ///         Messages = Messages.Build()
    ///             .WithInboundUrl("https://example.com/webhooks/inbound")
    ///             .WithStatusUrl("https://example.com/webhooks/status")
    ///     }
    /// };
    /// var application = await client.ApplicationClient.UpdateApplicationAsync(
    ///     "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
    ///     request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Application">More examples in the snippets repository</seealso>
    Task<Application> UpdateApplicationAsync(string id, CreateApplicationRequest request, Credentials creds = null);
}