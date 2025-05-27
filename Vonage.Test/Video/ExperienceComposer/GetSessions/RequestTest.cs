#region
using System;
using Vonage.Test.Common.Extensions;
using Vonage.Video.ExperienceComposer.GetSessions;
using Xunit;
#endregion

namespace Vonage.Test.Video.ExperienceComposer.GetSessions;

[Trait("Category", "Request")]
public class RequestTest
{
    [Theory]
    [InlineData(null, null, "/v2/project/e3e78a75-221d-41ec-8846-25ae3db1943a/render?offset=0&count=50")]
    [InlineData(100, null, "/v2/project/e3e78a75-221d-41ec-8846-25ae3db1943a/render?offset=0&count=100")]
    [InlineData(null, 100, "/v2/project/e3e78a75-221d-41ec-8846-25ae3db1943a/render?offset=100&count=50")]
    [InlineData(100, 100, "/v2/project/e3e78a75-221d-41ec-8846-25ae3db1943a/render?offset=100&count=100")]
    public void GetEndpointPath_ShouldReturnApiEndpoint(int? count, int? offset, string expectedEndpoint)
    {
        var builder = GetSessionsRequest.Build().WithApplicationId(new Guid("e3e78a75-221d-41ec-8846-25ae3db1943a"));
        if (count.HasValue)
        {
            builder = builder.WithCount(count.Value);
        }

        if (offset.HasValue)
        {
            builder = builder.WithOffset(offset.Value);
        }

        builder.Create().Map(request => request.BuildRequestMessage().RequestUri!.ToString()).Should()
            .BeSuccess(expectedEndpoint);
    }
}