using System.Net;
using System.Threading.Tasks;
using Vonage.ProactiveConnect.Lists;
using Vonage.ProactiveConnect.Lists.CreateList;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Lists.CreateList
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task CreateList()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWithMandatoryValues)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ProactiveConnectClient.CreateListAsync(CreateListRequest
                    .Build()
                    .WithName("my name")
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyList);
        }

        [Fact]
        public async Task CreateListWithManualDataSource()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWithManualDataSource)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ProactiveConnectClient.CreateListAsync(CreateListRequest
                    .Build()
                    .WithName("my name")
                    .WithDescription("my description")
                    .WithTag("vip")
                    .WithTag("sport")
                    .WithAttribute(new ListAttribute
                    {
                        Name = "phone_number",
                        Alias = "phone",
                    })
                    .WithDataSource(new ListDataSource
                    {
                        Type = ListDataSourceType.Manual,
                    })
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyList);
        }

        [Fact]
        public async Task CreateListWithSalesforceDataSource()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWithSalesforceDataSource)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ProactiveConnectClient.CreateListAsync(CreateListRequest
                    .Build()
                    .WithName("my name")
                    .WithDescription("my description")
                    .WithTag("vip")
                    .WithTag("sport")
                    .WithAttribute(new ListAttribute
                    {
                        Name = "phone_number",
                        Alias = "phone",
                    })
                    .WithDataSource(new ListDataSource
                    {
                        Type = ListDataSourceType.Salesforce,
                        Soql = "some sql",
                        IntegrationId = "123456789",
                    })
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyList);
        }
    }
}