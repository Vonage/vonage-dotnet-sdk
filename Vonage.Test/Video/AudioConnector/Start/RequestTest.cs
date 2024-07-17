#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Video.AudioConnector.Start;
using Xunit;
#endregion

namespace Vonage.Test.Video.AudioConnector.Start;

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
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/v2/project/301cf3c3-0027-4578-b212-dac7e924e85b/connect");
}