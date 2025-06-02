#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Video.ExperienceComposer.GetSession;
using Xunit;
#endregion

namespace Vonage.Test.Video.ExperienceComposer.GetSession;

[Trait("Category", "Request")]
public class RequestTest
{
    private const string ValidExperienceComposerId = "EXP-123";
    private readonly Guid validApplicationId = Guid.NewGuid();

    [Fact]
    public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        GetSessionRequest.Parse(Guid.Empty, ValidExperienceComposerId)
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_ShouldReturnFailure_GivenExperienceComposerIdIsEmpty(string invalidId) =>
        GetSessionRequest.Parse(Guid.NewGuid(), invalidId)
            .Should()
            .BeParsingFailure("ExperienceComposerId cannot be null or whitespace.");

    [Fact]
    public void Parse_ShouldSetApplicationId() =>
        GetSessionRequest.Parse(this.validApplicationId, ValidExperienceComposerId)
            .Map(request => request.ApplicationId)
            .Should()
            .BeSuccess(this.validApplicationId);

    [Fact]
    public void Parse_ShouldSetExperienceComposerId() =>
        GetSessionRequest.Parse(this.validApplicationId, ValidExperienceComposerId)
            .Map(request => request.ExperienceComposerId)
            .Should()
            .BeSuccess(ValidExperienceComposerId);

    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        GetSessionRequest.Parse(this.validApplicationId, ValidExperienceComposerId)
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess($"/v2/project/{this.validApplicationId}/render/EXP-123");
}