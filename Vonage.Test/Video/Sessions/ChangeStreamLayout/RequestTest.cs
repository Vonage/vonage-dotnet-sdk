using System;
using System.Collections.Generic;
using AutoFixture;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions.ChangeStreamLayout;
using Xunit;

namespace Vonage.Test.Video.Sessions.ChangeStreamLayout
{
    public class RequestTest
    {
        private readonly Guid applicationId;
        private readonly IEnumerable<ChangeStreamLayoutRequest.LayoutItem> items;
        private readonly ChangeStreamLayoutRequest.LayoutItem item1;
        private readonly ChangeStreamLayoutRequest.LayoutItem item2;
        private readonly string sessionId;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.item1 = fixture.Create<ChangeStreamLayoutRequest.LayoutItem>();
            this.item2 = fixture.Create<ChangeStreamLayoutRequest.LayoutItem>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            ChangeStreamLayoutRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithItem(this.item1)
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/stream");
    }
}