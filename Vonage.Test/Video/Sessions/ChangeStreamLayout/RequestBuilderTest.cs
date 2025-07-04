#region
using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions.ChangeStreamLayout;
using Xunit;
#endregion

namespace Vonage.Test.Video.Sessions.ChangeStreamLayout;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly Guid applicationId;
    private readonly ChangeStreamLayoutRequest.LayoutItem item1;
    private readonly ChangeStreamLayoutRequest.LayoutItem item2;
    private readonly IEnumerable<ChangeStreamLayoutRequest.LayoutItem> items;
    private readonly string sessionId;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.applicationId = fixture.Create<Guid>();
        this.sessionId = fixture.Create<string>();
        this.item1 = fixture.Create<ChangeStreamLayoutRequest.LayoutItem>();
        this.item2 = fixture.Create<ChangeStreamLayoutRequest.LayoutItem>();
    }

    [Fact]
    public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        ChangeStreamLayoutRequest.Build()
            .WithApplicationId(Guid.Empty)
            .WithSessionId(this.sessionId)
            .WithItems(new[] {this.item1})
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
        ChangeStreamLayoutRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(value)
            .WithItems(new[] {this.item1})
            .Create()
            .Should()
            .BeParsingFailure("SessionId cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
        ChangeStreamLayoutRequest.Build()
            .WithApplicationId(this.applicationId)
            .WithSessionId(this.sessionId)
            .WithItems(new[] {this.item1, this.item2})
            .Create()
            .Should()
            .BeSuccess(request =>
            {
                request.ApplicationId.Should().Be(this.applicationId);
                request.SessionId.Should().Be(this.sessionId);
                request.Items.Should().BeEquivalentTo(new[] {this.item1, this.item2});
            });
}