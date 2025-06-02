#region
using AutoFixture;
using FluentAssertions;
using Vonage.Meetings.GetRecordings;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Meetings.GetRecordings;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly string sessionId;

    public RequestTest()
    {
        var fixture = new Fixture();
        this.sessionId = fixture.Create<string>();
    }

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetRecordingsRequest.Parse(this.sessionId)
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v1/meetings/sessions/{this.sessionId}/recordings");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
        GetRecordingsRequest.Parse(value)
            .Should()
            .BeParsingFailure("SessionId cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
        GetRecordingsRequest.Parse(this.sessionId)
            .Should()
            .BeSuccess(request => request.SessionId.Should().Be(this.sessionId));
}