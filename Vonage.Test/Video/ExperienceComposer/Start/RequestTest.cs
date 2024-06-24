using System;
using Vonage.Server;
using Vonage.Test.Common.Extensions;
using Vonage.Video.ExperienceComposer.Start;
using Xunit;

namespace Vonage.Test.Video.ExperienceComposer.Start;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        StartRequest
            .Build()
            .WithApplicationId(new Guid("301cf3c3-0027-4578-b212-dac7e924e85b"))
            .WithSessionId("irrelevant")
            .WithToken("irrelevant")
            .WithUrl(new Uri("https://irrelevant.com"))
            .WithResolution(RenderResolution.StandardDefinitionLandscape)
            .WithName("irrelevant")
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v2/project/301cf3c3-0027-4578-b212-dac7e924e85b/render");
}