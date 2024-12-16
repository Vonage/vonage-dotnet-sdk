#region
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Vonage.Accounts;
using Vonage.Request;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;
#endregion

namespace Vonage.Test.Request;

public class ApiRequestTest : TestBase
{
    [Fact]
    public async Task Configuration_ShouldAllowCustomBaseUri()
    {
        var server = WireMockServer.Start();
        server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/rest/account/get-balance")
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBodyAsJson(new Balance())
                .WithStatusCode(HttpStatusCode.OK));
        this.configuration = Configuration.FromConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Url.Rest", $"{server.Url}/rest"},
            })
            .Build());
        var client = this.BuildVonageClient(Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret));
        await client.AccountClient.GetAccountBalanceAsync();
    }
}