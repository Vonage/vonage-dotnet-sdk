using System;
using System.IO.Abstractions;
using System.Net.Http;
using Vonage.Accounts;
using Vonage.Applications;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Conversions;
using Vonage.Meetings;
using Vonage.Messages;
using Vonage.Messaging;
using Vonage.NumberInsights;
using Vonage.Numbers;
using Vonage.Pricing;
using Vonage.ProactiveConnect;
using Vonage.Redaction;
using Vonage.Request;
using Vonage.ShortCodes;
using Vonage.SubAccounts;
using Vonage.Users;
using Vonage.Verify;
using Vonage.VerifyV2;
using Vonage.Voice;

namespace Vonage;

/// <summary>
///     Represents a client to use all features from Vonage's APIs.
/// </summary>
public class VonageClient
{
    private Credentials credentials;
    private readonly Maybe<Configuration> configuration = Maybe<Configuration>.None;

    public IAccountClient AccountClient { get; private set; }

    public IApplicationClient ApplicationClient { get; private set; }

    public IConversionClient ConversionClient { get; private set; }

    /// <summary>
    ///     Gets or sets credentials for this client.
    /// </summary>
    /// <remarks>Setting the value from this property will initialize all clients instances.</remarks>
    /// <exception cref="ArgumentNullException">When the value is null.</exception>
    public Credentials Credentials
    {
        get => this.credentials;
        set
        {
            this.credentials = value ?? throw new ArgumentNullException(nameof(this.Credentials));
            this.PropagateCredentials();
        }
    }

    /// <summary>
    ///     Exposes Meetings features.
    /// </summary>
    public IMeetingsClient MeetingsClient { get; private set; }

    public IMessagesClient MessagesClient { get; private set; }

    public INumberInsightClient NumberInsightClient { get; private set; }

    public INumbersClient NumbersClient { get; private set; }

    public IPricingClient PricingClient { get; private set; }

    /// <summary>
    ///     Exposes ProactiveConnect features.
    /// </summary>
    public IProactiveConnectClient ProactiveConnectClient { get; private set; }

    public IRedactClient RedactClient { get; private set; }

    public IShortCodesClient ShortCodesClient { get; private set; }

    public ISmsClient SmsClient { get; private set; }

    /// <summary>
    ///     Exposes SubAccounts features.
    /// </summary>
    public ISubAccountsClient SubAccountsClient { get; private set; }

    /// <summary>
    ///     Exposes User management features.
    /// </summary>
    public IUsersClient UsersClient { get; set; }

    public IVerifyClient VerifyClient { get; private set; }

    /// <summary>
    ///     Exposes VerifyV2 features.
    /// </summary>
    public IVerifyV2Client VerifyV2Client { get; private set; }

    public IVoiceClient VoiceClient { get; private set; }

    /// <summary>
    ///     Constructor for VonageClient.
    /// </summary>
    /// <param name="credentials">Credentials to be used for further HTTP calls.</param>
    public VonageClient(Credentials credentials) => this.Credentials = credentials;

    internal VonageClient(Credentials credentials, Configuration configuration)
    {
        this.configuration = configuration;
        this.Credentials = credentials;
    }
    
    internal VonageClient(Configuration configuration)
    {
        this.configuration = this.GetConfiguration();
        this.Credentials = configuration.BuildCredentials();
    }

    private VonageHttpClientConfiguration BuildConfiguration(Uri baseUri) =>
        new(
            InitializeHttpClient(baseUri),
            this.Credentials.GetAuthenticationHeader(),
            this.Credentials.GetUserAgent());

    private Configuration GetConfiguration() => this.configuration.IfNone(Configuration.Instance);

    private static HttpClient InitializeHttpClient(Uri baseUri)
    {
        var client = new HttpClient(new HttpClientHandler())
        {
            BaseAddress = baseUri,
        };
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        return client;
    }

    private void PropagateCredentials()
    {
        this.AccountClient = new AccountClient(this.Credentials, this.GetConfiguration());
        this.ApplicationClient = new ApplicationClient(this.Credentials, this.GetConfiguration());
        this.VoiceClient = new VoiceClient(this.Credentials, this.GetConfiguration());
        this.ConversionClient = new ConversionClient(this.Credentials, this.GetConfiguration());
        this.NumbersClient = new NumbersClient(this.Credentials, this.GetConfiguration());
        this.NumberInsightClient = new NumberInsightClient(this.Credentials, this.GetConfiguration());
        this.VerifyClient = new VerifyClient(this.Credentials, this.GetConfiguration());
        this.ShortCodesClient = new ShortCodesClient(this.Credentials, this.GetConfiguration());
        this.RedactClient = new RedactClient(this.Credentials, this.GetConfiguration());
        this.SmsClient = new SmsClient(this.Credentials, this.GetConfiguration());
        this.PricingClient = new PricingClient(this.Credentials, this.GetConfiguration());
        this.MessagesClient = new MessagesClient(this.Credentials, this.GetConfiguration());
        this.VerifyV2Client = new VerifyV2Client(this.BuildConfiguration(this.GetConfiguration().NexmoApiUrl));
        this.SubAccountsClient = new SubAccountsClient(this.BuildConfiguration(this.GetConfiguration().NexmoApiUrl),
            this.Credentials.ApiKey);
        this.UsersClient = new UsersClient(this.BuildConfiguration(this.GetConfiguration().NexmoApiUrl));
        this.MeetingsClient = new MeetingsClient(this.BuildConfiguration(this.GetConfiguration().EuropeApiUrl),
            new FileSystem());
        this.ProactiveConnectClient =
            new ProactiveConnectClient(this.BuildConfiguration(this.GetConfiguration().EuropeApiUrl));
    }
}