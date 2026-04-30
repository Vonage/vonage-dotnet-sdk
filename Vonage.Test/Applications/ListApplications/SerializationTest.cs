using FluentAssertions;
using Vonage.Applications.ListApplications;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Applications.ListApplications;

[Trait("Category", "Serialization")]
[Trait("Product", "ApplicationsNew")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<ListApplicationsResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(response =>
            {
                response.PageSize.Should().Be(10);
                response.Page.Should().Be(1);
                response.TotalItems.Should().Be(1);
                response.TotalPages.Should().Be(1);
                response.Embedded.Applications.Should().HaveCount(1);
                response.Embedded.Applications[0].Id.Should().Be("78d335fa-323d-0114-9c3d-d6f0d48968cf");
                response.Embedded.Applications[0].Name.Should().Be("My Application");
            });
}
