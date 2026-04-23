#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Reports;
using Vonage.Reports.LoadRecords;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Reports.LoadRecords;

[Trait("Category", "E2E")]
[Trait("Product", "Reports")]
public class E2ETest : E2EBase
{
    private readonly SerializationTestHelper localSerialization =
        new SerializationTestHelper(typeof(E2ETest).Namespace, JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task LoadRecords()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/reports/records")
                .WithParam("product", "SMS")
                .WithParam("account_id", "12aa3456")
                .WithParam("direction", "outbound")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.localSerialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.ReportsClient
            .LoadRecordsAsync(LoadRecordsRequest.Build()
                .WithProduct(ReportProduct.Sms)
                .WithAccountId("12aa3456")
                .WithDirection(RecordDirection.Outbound)
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task LoadRecords_ShouldFollowNextLink()
    {
        var responseBody =
            this.localSerialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200));

        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/reports/records")
                .WithParam("product", "SMS")
                .WithParam("account_id", "12aa3456")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK).WithBody(responseBody));
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/reports/records")
                .WithParam("product", "SMS")
                .WithParam("account_id", "12aa3456")
                .WithParam("direction", "outbound")
                .WithParam("cursor", "MTY0OTQ3ODAwMDAwMA")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK).WithBody(responseBody));

        var firstPage = await this.Helper.VonageClient.ReportsClient
            .LoadRecordsAsync(LoadRecordsRequest.Build()
                .WithProduct(ReportProduct.Sms)
                .WithAccountId("12aa3456")
                .Create());

        await this.Helper.VonageClient.ReportsClient
            .LoadRecordsAsync(firstPage.Bind(response => response.Links.Next.BuildRequest()))
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}
