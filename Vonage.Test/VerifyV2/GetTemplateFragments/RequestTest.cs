#region
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.GetTemplateFragments;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplateFragments;

[Trait("Category", "Request")]
public class RequestTest
{
    [Theory]
    [InlineData(null, null, "/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments")]
    [InlineData(30, null, "/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments?page_size=30")]
    [InlineData(null, 50, "/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments?page=50")]
    [InlineData(30, 50,
        "/v2/verify/templates/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb/template_fragments?page_size=30&page=50")]
    public void GetEndpointPath_ShouldReturnApiEndpoint(int? pageSize, int? page, string expectedEndpoint)
    {
        var builder = GetTemplateFragmentsRequest.Build().WithTemplateId(RequestBuilderTest.ValidTemplateId);
        if (pageSize.HasValue)
        {
            builder = builder.WithPageSize(pageSize.Value);
        }

        if (page.HasValue)
        {
            builder = builder.WithPage(page.Value);
        }

        builder.Create().Map(request => request.GetEndpointPath()).Should().BeSuccess(expectedEndpoint);
    }
}