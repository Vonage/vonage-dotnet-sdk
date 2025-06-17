﻿#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
#endregion

namespace Vonage.NumberInsights;

public class NumberInsightClient : INumberInsightClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    public NumberInsightClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal NumberInsightClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    public Credentials Credentials { get; set; }

    public void ValidateNumberInsightResponse(NumberInsightResponseBase response)
    {
        if (response.Status != 0)
        {
            switch (response)
            {
                case AdvancedInsightsAsynchronousResponse asyncResponse:
                    throw new VonageNumberInsightResponseException(
                            $"Advanced Insights Async response failed with status: {asyncResponse.Status}")
                        {Response = response};
                case BasicInsightResponse basicInsightResponse:
                    throw new VonageNumberInsightResponseException(
                            $"Number insight request failed with status: {basicInsightResponse.Status} and error message: {basicInsightResponse.StatusMessage}")
                        {Response = response};
            }
        }
    }

    /// <inheritdoc/>
    public async Task<AdvancedInsightsResponse> GetNumberInsightAdvancedAsync(AdvancedNumberInsightRequest request,
        Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<AdvancedInsightsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/ni/advanced/json"),
                AuthType.Basic,
                request
            ).ConfigureAwait(false);
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public async Task<AdvancedInsightsAsynchronousResponse> GetNumberInsightAsynchronousAsync(
        AdvancedNumberInsightAsynchronousRequest request, Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<AdvancedInsightsAsynchronousResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/ni/advanced/async/json"),
                AuthType.Basic,
                request
            ).ConfigureAwait(false);
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public async Task<BasicInsightResponse> GetNumberInsightBasicAsync(BasicNumberInsightRequest request,
        Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<BasicInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/ni/basic/json"),
                AuthType.Basic,
                request
            ).ConfigureAwait(false);
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    /// <inheritdoc/>
    public async Task<StandardInsightResponse> GetNumberInsightStandardAsync(StandardNumberInsightRequest request,
        Credentials creds = null)
    {
        var response = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<StandardInsightResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/ni/standard/json"),
                AuthType.Basic,
                request
            ).ConfigureAwait(false);
        this.ValidateNumberInsightResponse(response);
        return response;
    }

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}