#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.DeleteAddress;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.DeleteAddress;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task DeleteAddressAsync()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"/v1/addresses/{Constants.ValidAddressId}")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingDelete())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.NoContent));
        await this.Helper.VonageClient.EmergencyClient
            .DeleteAddressAsync(DeleteAddressRequest.Build().WithId(new Guid(Constants.ValidAddressId)).Create())
            .Should()
            .BeSuccessAsync();
    }
}