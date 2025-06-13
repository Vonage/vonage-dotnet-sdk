#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Video.LiveCaptions.Start;
using Xunit;
#endregion

namespace Vonage.Test.Video.LiveCaptions.Start;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void ReqeustUri_ShouldReturnApiEndpoint() =>
        StartRequest
            .Build()
            .WithApplicationId(new Guid("301cf3c3-0027-4578-b212-dac7e924e85b"))
            .WithSessionId("irrelevant")
            .WithToken("irrelevant")
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/project/301cf3c3-0027-4578-b212-dac7e924e85b/captions");
}