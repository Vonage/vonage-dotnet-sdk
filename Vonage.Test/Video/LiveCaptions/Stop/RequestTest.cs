#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Video.LiveCaptions.Stop;
using Xunit;
#endregion

namespace Vonage.Test.Video.LiveCaptions.Stop;

[Trait("Category", "Request")]
public class RequestTest
{
    private const string ValidCaptionsId = "CAP-123";
    private readonly Guid validApplicationId = Guid.NewGuid();

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        StopRequest.Parse(new Guid("301cf3c3-0027-4578-b212-dac7e924e85b"), "CAP-123")
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/project/301cf3c3-0027-4578-b212-dac7e924e85b/captions/CAP-123/stop");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        StopRequest.Parse(Guid.Empty, ValidCaptionsId)
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenCaptionsIdIsEmpty(string invalidId) =>
        StopRequest.Parse(Guid.NewGuid(), invalidId)
            .Should()
            .BeParsingFailure("CaptionsId cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldSetApplicationId() =>
        StopRequest.Parse(this.validApplicationId, ValidCaptionsId)
            .Map(request => request.ApplicationId)
            .Should()
            .BeSuccess(this.validApplicationId);

    [Fact]
    public void Parse_ShouldSetCaptionsId() =>
        StopRequest.Parse(this.validApplicationId, ValidCaptionsId)
            .Map(request => request.CaptionsId)
            .Should()
            .BeSuccess(ValidCaptionsId);
}