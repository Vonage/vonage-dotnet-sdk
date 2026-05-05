using System.Linq;
using System.Web;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Xunit;
using ListApplicationsRequest = Vonage.Applications.ListApplications.ListApplicationsRequest;

namespace Vonage.Test.Applications.ListApplications;

[Trait("Category", "Request")]
[Trait("Product", "ApplicationsNew")]
public class RequestTest
{
    [Fact]
    public void RequestUri_ShouldReturnApiEndpoint() =>
        ListApplicationsRequest.Build()
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess("/v2/applications");

    [Fact]
    public void RequestUri_ShouldContainAllParameters() =>
        ListApplicationsRequest.Build()
            .WithPageSize(20)
            .WithPage(3)
            .Create()
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
            .Should()
            .BeSuccess(uri =>
            {
                var q = HttpUtility.ParseQueryString(uri.Split('?').Last());
                q["page_size"].Should().Be("20");
                q["page"].Should().Be("3");
            });
}
