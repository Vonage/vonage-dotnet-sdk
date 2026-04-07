#region
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
#endregion

namespace Vonage.Redaction;

/// <summary>
///     Represents a client for the Vonage Redact API, enabling removal of personal data from the Vonage platform.
/// </summary>
public class RedactClient : IRedactClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    /// <summary>
    ///     Initializes a new instance of the <see cref="RedactClient"/> class.
    /// </summary>
    /// <param name="creds">Optional credentials to use for authentication.</param>
    public RedactClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal RedactClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    /// <summary>
    ///     Gets or sets the credentials used for authenticating API requests.
    /// </summary>
    public Credentials Credentials { get; set; }

    /// <inheritdoc/>
    public async Task<bool> RedactAsync(RedactRequest request, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<object>
            (
                HttpMethod.Post,
                this.configuration.BuildUri(ApiRequest.UriType.Api, "/v1/redact/transaction"),
                request,
                AuthType.Basic
            ).ConfigureAwait(false);
        return true;
    }

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}