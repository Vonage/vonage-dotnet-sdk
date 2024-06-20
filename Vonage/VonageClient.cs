using System;
using System.IO.Abstractions;
using System.Net.Http;
using Vonage.Accounts;
using Vonage.Applications;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversions;
using Vonage.Meetings;
using Vonage.Messages;
using Vonage.Messaging;
using Vonage.NumberInsights;
using Vonage.NumberInsightV2;
using Vonage.Numbers;
using Vonage.NumberVerification;
using Vonage.Pricing;
using Vonage.ProactiveConnect;
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
        this.configuration = this.GetConfiguration();
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

    /// <summary>
    ///     Exposes Meetings features.
    /// </summary>
    public IMeetingsClient MeetingsClient { get; private set; }

    public IMessagesClient MessagesClient { get; private set; }

    public INumberInsightClient NumberInsightClient { get; private set; }

    /// <summary>
    ///     Exposes Number Insight V2 features.
    /// </summary>
    public INumberInsightV2Client NumberInsightV2Client { get; private set; }

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

    public ISimSwapClient SimSwapClient { get; private set; }

    public INumberVerificationClient NumberVerificationClient { get; private set; }

    private VonageHttpClientConfiguration BuildConfiguration(HttpClient client) =>
        new VonageHttpClientConfiguration(client, this.Credentials.GetAuthenticationHeader(),
            this.Credentials.GetUserAgent());

    private Configuration GetConfiguration() => this.configuration.IfNone(Configuration.Instance);

    private void PropagateCredentials()
    {
        var currentConfiguration = this.GetConfiguration();
        this.AccountClient = new AccountClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.ApplicationClient = new ApplicationClient(this.Credentials, currentConfiguration, this.timeProvider);
        this.VoiceClient = new VoiceClient(this.Credentials, currentConfiguration, this.timeProvider);
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
        var nexmoConfiguration = this.BuildConfiguration(currentConfiguration.BuildHttpClientForNexmo());
        var videoConfiguration = this.BuildConfiguration(currentConfiguration.BuildHttpClientForVideo());
        var euConfiguration =
            this.BuildConfiguration(currentConfiguration.BuildHttpClientForRegion(VonageUrls.Region.EU));
        var oidcConfiguration = this.BuildConfiguration(currentConfiguration.BuildHttpClientForOidc());
        this.VerifyV2Client = new VerifyV2Client(nexmoConfiguration);
        this.SubAccountsClient = new SubAccountsClient(nexmoConfiguration, this.Credentials.ApiKey);
        this.NumberInsightV2Client = new NumberInsightV2Client(nexmoConfiguration);
        this.UsersClient = new UsersClient(nexmoConfiguration);
        this.ConversationsClient = new ConversationsClient(nexmoConfiguration);
        this.MeetingsClient = new MeetingsClient(euConfiguration, new FileSystem());
        this.ProactiveConnectClient = new ProactiveConnectClient(euConfiguration);
        this.SimSwapClient = new SimSwapClient(euConfiguration);
        this.NumberVerificationClient = new NumberVerificationClient(euConfiguration, oidcConfiguration);
        this.VideoClient = new VideoClient(videoConfiguration);
    }
}