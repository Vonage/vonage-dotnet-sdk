using System;
using System.Net.Http;
using Vonage.Accounts;
using Vonage.Applications;
using Vonage.Conversions;
using Vonage.Messages;
using Vonage.Messaging;
using Vonage.NumberInsights;
using Vonage.Numbers;
using Vonage.Pricing;
using Vonage.Redaction;
using Vonage.Request;
using Vonage.ShortCodes;
using Vonage.Verify;
using Vonage.Voice;

namespace Vonage;

/// <summary>
///     Represents a client to use all features from Vonage's APIs.
/// </summary>
public class VonageClient
{
    private Credentials credentials;

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

    public IMessagesClient MessagesClient { get; private set; }

    public INumberInsightClient NumberInsightClient { get; private set; }

    public INumbersClient NumbersClient { get; private set; }

    public IPricingClient PricingClient { get; private set; }

    public IRedactClient RedactClient { get; private set; }

    public IShortCodesClient ShortCodesClient { get; private set; }

    public ISmsClient SmsClient { get; private set; }

    public IVerifyClient VerifyClient { get; private set; }

    public IVoiceClient VoiceClient { get; private set; }

    /// <summary>
    ///     Constructor for VonageClient.
    /// </summary>
    /// <param name="credentials">Credentials to be used for further HTTP calls.</param>
    public VonageClient(Credentials credentials) => this.Credentials = credentials;

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
        this.AccountClient = new AccountClient(this.Credentials);
        this.ApplicationClient = new ApplicationClient(this.Credentials);
        this.VoiceClient = new VoiceClient(this.Credentials);
        this.ConversionClient = new ConversionClient(this.Credentials);
        this.NumbersClient = new NumbersClient(this.Credentials);
        this.NumberInsightClient = new NumberInsightClient(this.Credentials);
        this.VerifyClient = new VerifyClient(this.Credentials);
        this.ShortCodesClient = new ShortCodesClient(this.Credentials);
        this.RedactClient = new RedactClient(this.Credentials);
        this.SmsClient = new SmsClient(this.Credentials);
        this.PricingClient = new PricingClient(this.Credentials);
        this.MessagesClient = new MessagesClient(this.Credentials);
    }
}