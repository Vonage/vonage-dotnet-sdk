#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Video.ExperienceComposer.Stop;
using Xunit;
#endregion

namespace Vonage.Test.Video.ExperienceComposer.Stop;

[Trait("Category", "Request")]
public class RequestTest
{
    private const string ValidExperienceComposerId = "EXP-123";
    private readonly Guid validApplicationId = Guid.NewGuid();

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        StopRequest.Parse(this.validApplicationId, ValidExperienceComposerId)
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.validApplicationId}/render/EXP-123");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        StopRequest.Parse(Guid.Empty, ValidExperienceComposerId)
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenExperienceComposerIdIsEmpty(string invalidId) =>
        StopRequest.Parse(Guid.NewGuid(), invalidId)
            .Should()
            .BeParsingFailure("ExperienceComposerId cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldSetApplicationId() =>
        StopRequest.Parse(this.validApplicationId, ValidExperienceComposerId)
            .Map(request => request.ApplicationId)
            .Should()
            .BeSuccess(this.validApplicationId);

    [Fact]
    public void Parse_ShouldSetExperienceComposerId() =>
        StopRequest.Parse(this.validApplicationId, ValidExperienceComposerId)
            .Map(request => request.ExperienceComposerId)
            .Should()
            .BeSuccess(ValidExperienceComposerId);
}