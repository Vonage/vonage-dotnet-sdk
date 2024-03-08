using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;

namespace Vonage.Redaction;

public class RedactClient : IRedactClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

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

    public Credentials Credentials { get; set; }

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public bool Redact(RedactRequest request, Credentials creds = null)
    {
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContent<object>
            (
                HttpMethod.Post,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/v1/redact/transaction"),
                request,
                AuthType.Basic
            );
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> RedactAsync(RedactRequest request, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<object>
            (
                HttpMethod.Post,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/v1/redact/transaction"),
                request,
                AuthType.Basic
            );
        return true;
    }

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}