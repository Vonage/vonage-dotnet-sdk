#region
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.GetTemplates;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplates;

[Trait("Category", "Request")]
public class RequestTest
{
    [Theory]
    [InlineData(null, null, "/v2/verify/templates")]
    [InlineData(30, null, "/v2/verify/templates?page_size=30")]
    [InlineData(null, 50, "/v2/verify/templates?page=50")]
    [InlineData(30, 50, "/v2/verify/templates?page_size=30&page=50")]
    public void GetEndpointPath_ShouldReturnApiEndpoint(int? pageSize, int? page, string expectedEndpoint)
    {
        var builder = GetTemplatesRequest.Build();
        if (pageSize.HasValue)
        {
            builder = builder.WithPageSize(pageSize.Value);
        }

        if (page.HasValue)
        {
            builder = builder.WithPage(page.Value);
        }

        builder.Create().Map(request => request.BuildRequestMessage().RequestUri!.ToString()).Should()
            .BeSuccess(expectedEndpoint);
    }
}