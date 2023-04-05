using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sessions.ChangeStreamLayout;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.ChangeStreamLayout
{
    public class RequestTest
    {
        private readonly Guid applicationId;
        private readonly IEnumerable<ChangeStreamLayoutRequest.LayoutItem> items;
        private readonly string sessionId;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.items = fixture.CreateMany<ChangeStreamLayoutRequest.LayoutItem>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            ChangeStreamLayoutRequest.Parse(this.applicationId, this.sessionId, this.items)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/stream");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            ChangeStreamLayoutRequest.Parse(Guid.Empty, this.sessionId, this.items)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenItemsIsNull() =>
            ChangeStreamLayoutRequest.Parse(this.applicationId, this.sessionId, null)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Items cannot be null."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            ChangeStreamLayoutRequest.Parse(this.applicationId, value, this.items)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            ChangeStreamLayoutRequest.Parse(this.applicationId, this.sessionId, this.items)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.Items.Should().BeEquivalentTo(this.items);
                });
    }
}