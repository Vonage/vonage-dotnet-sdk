#region
using System;
using System.IO.Abstractions;
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.NumberVerification;
using Vonage.Request;
using Vonage.SimSwap;
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



    public ISimSwapClient SimSwapClient { get; private set; }

    public INumberVerificationClient NumberVerificationClient { get; private set; }

    private VonageHttpClientConfiguration BuildConfiguration(HttpClient client) =>
        new VonageHttpClientConfiguration(client, this.Credentials.GetAuthenticationHeader(),
            this.Credentials.GetUserAgent());

    private Configuration GetConfiguration() => this.configuration.IfNone(Configuration.Instance);

    private void PropagateCredentials()
    {
        var currentConfiguration = this.GetConfiguration();
        var nexmoConfiguration = this.BuildConfiguration(currentConfiguration.BuildHttpClientForNexmo());
        var videoConfiguration = this.BuildConfiguration(currentConfiguration.BuildHttpClientForVideo());
        var euConfiguration =
            this.BuildConfiguration(currentConfiguration.BuildHttpClientForRegion(VonageUrls.Region.EU));
        var oidcConfiguration = this.BuildConfiguration(currentConfiguration.BuildHttpClientForOidc());
        this.SimSwapClient = new SimSwapClient(euConfiguration);
        this.NumberVerificationClient = new NumberVerificationClient(euConfiguration, oidcConfiguration);
    }
}