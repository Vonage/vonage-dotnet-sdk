using System;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;

namespace Vonage.Conversions;

public class ConversionClient : IConversionClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

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

    public Credentials Credentials { get; set; }

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public bool SmsConversion(ConversionRequest request, Credentials creds = null)
    {
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObject<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/conversions/sms"),
                request
            );
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> SmsConversionAsync(ConversionRequest request, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/conversions/sms"),
                request
            );
        return true;
    }

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public bool VoiceConversion(ConversionRequest request, Credentials creds = null)
    {
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObject<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/conversions/voice"),
                request
            );
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> VoiceConversionAsync(ConversionRequest request, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/conversions/voice"),
                request
            );
        return true;
    }

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}