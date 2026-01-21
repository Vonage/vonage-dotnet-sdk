#region
using System;
using System.Net.Http;
using Vonage.Accounts;
using Vonage.Applications;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversions;
using Vonage.Messages;
using Vonage.Messaging;
using Vonage.NumberInsights;
using Vonage.NumberInsightV2;
using Vonage.Numbers;
using Vonage.NumberVerification;
using Vonage.Pricing;
using Vonage.Redaction;
using Vonage.Request;
using Vonage.ShortCodes;
using Vonage.SimSwap;
using Vonage.SubAccounts;
using Vonage.Users;
using Vonage.Verify;
using Vonage.VerifyV2;
using Vonage.Video;
using Vonage.Voice;
#endregion

namespace Vonage;

/// <summary>
///     Represents a client to use all features from Vonage's APIs.
/// </summary>
public class VonageClient
{
    private readonly Maybe<Configuration> configuration = Maybe<Configuration>.None;
    private readonly ITimeProvider timeProvider = new TimeProvider();
    private Credentials credentials;

    /// <summary>
    ///     Constructor for VonageClient.
    /// </summary>
    /// <param name="credentials">Credentials to be used for further HTTP calls.</param>
    public VonageClient(Credentials credentials) => this.Credentials = credentials;

    internal VonageClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.timeProvider = timeProvider;
        this.configuration = configuration;
        this.Credentials = credentials;
    }

    internal VonageClient(Configuration configuration)
    {
        this.configuration = configuration;
        this.Credentials = configuration.BuildCredentials();
    }

    public IAccountClient AccountClient { get; private set; }

    public IApplicationClient ApplicationClient { get; private set; }

    /// <summary>
    ///     Exposes Conversations features.
    /// </summary>
    public IConversationsClient ConversationsClient { get; private set; }

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

    public IMessagesClient MessagesClient { get; private set; }

    public INumberInsightClient NumberInsightClient { get; private set; }

    /// <summary>
    ///     Exposes Number Insight V2 features.
    /// </summary>
    public INumberInsightV2Client NumberInsightV2Client { get; private set; }

    public INumbersClient NumbersClient { get; private set; }

    public INumberVerificationClient NumberVerificationClient { get; private set; }

    public IPricingClient PricingClient { get; private set; }

    public IRedactClient RedactClient { get; private set; }

    public IShortCodesClient ShortCodesClient { get; private set; }

    public ISimSwapClient SimSwapClient { get; private set; }

    public ISmsClient SmsClient { get; private set; }

    /// <summary>
    ///     Exposes SubAccounts features.
    /// </summary>
    public ISubAccountsClient SubAccountsClient { get; private set; }

    /// <summary>
    ///     Exposes User management features.
    /// </summary>
    public IUsersClient UsersClient { get; private set; }

    public IVerifyClient VerifyClient { get; private set; }

    /// <summary>
    ///     Exposes VerifyV2 features.
    /// </summary>
    public IVerifyV2Client VerifyV2Client { get; private set; }

    /// <summary>
    ///     Exposes Video features.
    /// </summary>
    public IVideoClient VideoClient { get; private set; }

    public IVoiceClient VoiceClient { get; private set; }

    private VonageHttpClientConfiguration BuildConfiguration(HttpClient client) =>
        new VonageHttpClientConfiguration(client, this.Credentials.GetAuthenticationHeader(),
            this.Credentials.GetUserAgent());

    private VonageHttpClientConfiguration BuildConfiguration(HttpClient client, AuthType preferredAuthType) =>
        new VonageHttpClientConfiguration(client, this.Credentials.GetPreferredAuthenticationHeader(preferredAuthType),
            this.Credentials.GetUserAgent());

    private Configuration GetConfiguration() => this.configuration.IfNone(Configuration.Instance);

    private void PropagateCredentials()
    {
        var currentConfiguration = this.GetConfiguration();
        this.AccountClient = new AccountClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.ApplicationClient = new ApplicationClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.VoiceClient = new VoiceClient(this.Credentials, currentConfiguration, this.timeProvider,
            Maybe<VonageUrls.Region>.None);
        this.ConversionClient = new ConversionClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.NumbersClient = new NumbersClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.NumberInsightClient =
            new NumberInsightClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.VerifyClient = new VerifyClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.ShortCodesClient = new ShortCodesClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.RedactClient = new RedactClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.SmsClient = new SmsClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.PricingClient = new PricingClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.MessagesClient = new MessagesClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.VerifyV2Client =
            new VerifyV2Client(this.BuildConfiguration(currentConfiguration.BuildHttpClientForNexmo()));
        this.SubAccountsClient =
            new SubAccountsClient(
                this.BuildConfiguration(currentConfiguration.BuildHttpClientForNexmo(), AuthType.Basic),
                this.Credentials.ApiKey);
        this.NumberInsightV2Client =
            new NumberInsightV2Client(this.BuildConfiguration(currentConfiguration.BuildHttpClientForNexmo()));
        this.UsersClient = new UsersClient(this.BuildConfiguration(currentConfiguration.BuildHttpClientForNexmo()));
        this.ConversationsClient =
            new ConversationsClient(this.BuildConfiguration(currentConfiguration.BuildHttpClientForNexmo(),
                AuthType.Bearer));
        this.SimSwapClient =
            new SimSwapClient(
                this.BuildConfiguration(currentConfiguration.BuildHttpClientForRegion(VonageUrls.Region.EU),
                    AuthType.Bearer));
        this.NumberVerificationClient = new NumberVerificationClient(
            this.BuildConfiguration(currentConfiguration.BuildHttpClientForRegion(VonageUrls.Region.EU)),
            this.BuildConfiguration(currentConfiguration.BuildHttpClientForOidc()));
        this.VideoClient =
            new VideoClient(this.BuildConfiguration(currentConfiguration.BuildHttpClientForVideo(), AuthType.Bearer));
    }
}