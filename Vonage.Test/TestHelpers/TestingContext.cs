#region
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using WireMock.Server;
using TimeProvider = Vonage.Common.TimeProvider;
#endregion

namespace Vonage.Test.TestHelpers;

internal sealed class TestingContext : IDisposable
{
    private const string ExpectedBasicHeaderValue = "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5";
    private const string ExpectedBearerHeaderValue = "Bearer *";
    private const string ApiKey = "790fc5e5";
    private const string ApiSecret = "Aa3456789";
    private const string ApplicationId = "b575cd11-7db2-4161-ac63-eeb82033ec17";

    private const string ApplicationKey =
        "-----BEGIN PRIVATE KEY----- MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQC5tb9yvkGc/CAq dl3YzyixNc8kBQzTsLXEKtXmrZGsuyFgnaI4ea4sLj7Woxg+T7GR7ZTQpkFLiLvv vWXnTBHcF/sU3aW4NWkHZV2XUMFY8wvyjdq/T0x1sz0jHIanyRJQIbcIB5MeNeaS 8byimsKvVv82sW7fNDDgGMdbLIr32H82D5egS7K5ZZRQNkzU6MmsI+BGe9vehQ7e OE0pkRzghmmIe1thxX8BRMVcCRcRdgnJ+IFqeCHcpq4wovhD2O7Wix19JepJDv6J /aS8VP70G/OATOeGaELsZmeRDhXqqtB47ywr8h2dHSwlc3iP9vn3mfINqmgCNpbx mWeTkv+JAgMBAAECggEAAZL4TfUt4jJC5Yk+T2WmHyZpHJGMY8j2KXUBfiSPfQrT BBoh81+Yqdg0gSY9wN11EFsWrVQTbpv4l9YfpH/BEQAFCU69pluRidYCnpM5KFNb ERHBvmhiKgc8Z+1IeOib5qD6h69kwFdNnoY9BpXQO6M9o42KdorfNS8QlYXBocso yp2GcGfK06zWMC3duPTURRz0en4Fp8PXCd9DB+SKxp/8OG7RL9sdFmkw2+XtC0jk DqSxQ+v7K3g0BcrOIa3dhoqNmPyzSGwiDnPFO9dpnzHYa9GscMxS9UV0KFhYj+Xn 4ssqRjj5F9wMNdXlE4qk1pp0/+vOqcAY8Q7tEOfOjwKBgQDaXf4LZdkDxgAGxuA2 3r6WhjR/+ijP/MXnadyro6XDDXb4Ru1/d5hVnnikS/dZNvvvrYS3NULS37DdzM09 OEC0OjsboAJFmA60lw0ij4Bs/U2AwNNI5UVby7s/AIT6Y/DAM1l+WkVggfIt2gU2 +EaYrNczo9rbGhfP7jhRuB9sWwKBgQDZtvjeD3uQFs51VbJM4lMcXOcJ6e75i80R 7WwJJGqyddldPzuK9N4nEgVhT03onXHhLk9s1XgWyaJnjLL4zk21PRyPGDh8xYwq V3vUJ3aTERb6ZC8SxpLFNiN42DIM5a0HbICt3uzhlxITUSSPvDmJgIsqE7izIWAh CW5kglYY6wKBgQCynr/3ws/JbmUHJhaxy3JK3myDYrWPrEyWBtoi7DHjY1g1ro8G /WT2ZDJ68kjaCUf7vgwZcM/AfonGZIhd023Z/ufqqPAyzTb6MbTk2E1M2cZT02cA 8cnSVMxNtLcRuj5seZRy7pRhZOoc54HsfRoCOR+vdhDHuIhR5aLb9ah+kQKBgQCb ixPGYr1etkyOm8klENVcACu4c3+enfjHBB8ZcQEhuvyumAyMPGGy/DcHzMbWmBXS UWnBUcnYTfpPjMAY7huqjpymxyEkU2bOoW4ApqkabS1Deuv+uAwIBaPWJG+tszGp iiVtE0Wd7nalgmVio5Ff4YyLZUeiAwhQ0hIikNO+PQKBgF+ZtWH32tI0+xn5k9Ib IYppbs8CUVkIWvMPBpeUJlLJbud9hEGNi/qdTBewDMKHOeYF4gHKTxCAf6lszKHO In3DRwnnurpODEmXQdH4FW8N2YVxa/LnUKeVj7HEL71HqbeB3cK313dsu5dvXPBS +O6wcZ4Yllb740vNKfuf/wfa -----END PRIVATE KEY-----";

    private TestingContext(string authorizationHeaderValue, Dictionary<string, string> settings)
    {
        this.ExpectedAuthorizationHeaderValue = authorizationHeaderValue;
        this.Server = WireMockServer.Start();
        settings.Add(VonageUrls.NexmoApiKey, this.Server.Url);
        settings.Add($"{VonageUrls.NexmoApiKey}.EMEA", this.Server.Url);
        settings.Add($"{VonageUrls.NexmoApiKey}.APAC", this.Server.Url);
        settings.Add($"{VonageUrls.NexmoApiKey}.AMER", this.Server.Url);
        settings.Add(VonageUrls.NexmoRestKey, this.Server.Url);
        settings.Add(VonageUrls.VideoApiKey, this.Server.Url);
        settings.Add(VonageUrls.OidcApiKey, this.Server.Url);
        var configuration =
            Configuration.FromConfiguration(new ConfigurationBuilder().AddInMemoryCollection(settings).Build());
        this.VonageClient = new VonageClient(configuration.BuildCredentials(), configuration, new TimeProvider());
    }

    public string ExpectedAuthorizationHeaderValue { get; }
    public WireMockServer Server { get; }
    public VonageClient VonageClient { get; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public static TestingContext WithBasicCredentials() =>
        new TestingContext(ExpectedBasicHeaderValue,
            new Dictionary<string, string>
            {
                {"vonage:Api.Key", ApiKey},
                {"vonage:Api.Secret", ApiSecret},
            });

    public static TestingContext WithAllCredentials() =>
        new TestingContext(ExpectedBasicHeaderValue,
            new Dictionary<string, string>
            {
                {"vonage:Api.Key", ApiKey},
                {"vonage:Api.Secret", ApiSecret},
                {"vonage:Application.Id", ApplicationId},
                {"vonage:Application.Key", ApplicationKey},
            });

    public static TestingContext WithBearerCredentials() =>
        new TestingContext(ExpectedBearerHeaderValue,
            new Dictionary<string, string>
            {
                {"vonage:Application.Id", ApplicationId},
                {"vonage:Application.Key", ApplicationKey},
            });

    public static TestingContext WithBasicCredentials(Dictionary<string, string> settings)
    {
        var options = new Dictionary<string, string>(settings)
        {
            {"vonage:Api.Key", ApiKey},
            {"vonage:Api.Secret", ApiSecret},
        };
        return new TestingContext(ExpectedBasicHeaderValue, options);
    }

    private void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        this.Server.Stop();
        this.Server.Dispose();
    }
}