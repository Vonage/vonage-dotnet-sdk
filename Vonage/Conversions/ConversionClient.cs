#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
#endregion

namespace Vonage.Conversions;

/// <summary>
///     Implementation of <see cref="IConversionClient"/> for submitting conversion data to the Vonage Conversion API.
/// </summary>
public class ConversionClient : IConversionClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    /// <summary>
    ///     Initializes a new instance of the <see cref="ConversionClient"/> class.
    /// </summary>
    /// <param name="creds">Optional credentials to use for API requests.</param>
    public ConversionClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal ConversionClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    /// <summary>
    ///     Gets or sets the credentials used to authenticate API requests.
    /// </summary>
    public Credentials Credentials { get; set; }

    /// <inheritdoc/>
    public async Task<bool> SmsConversionAsync(ConversionRequest request, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<object>
            (
                this.configuration.BuildUri(ApiRequest.UriType.Api, "/conversions/sms"),
                request
            ).ConfigureAwait(false);
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> VoiceConversionAsync(ConversionRequest request, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<object>
            (
                this.configuration.BuildUri(ApiRequest.UriType.Api, "/conversions/voice"),
                request
            ).ConfigureAwait(false);
        return true;
    }

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}