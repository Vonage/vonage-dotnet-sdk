﻿using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Events.GetEvents;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Events.GetEvents
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(SerializationTest).Namespace)
        {
        }

        [Fact]
        public async Task GetEvents()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/events")
                    .WithParam("page", "25")
                    .WithParam("page_size", "50")
                    .WithParam("order", "asc")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.ProactiveConnectClient.GetEventsAsync(GetEventsRequest
                .Build()
                .WithPage(25)
                .WithPageSize(50)
                .Create());
            result.Should().BeSuccess();
        }
    }
}