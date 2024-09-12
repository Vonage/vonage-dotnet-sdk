#region
using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2;
using Vonage.VerifyV2.GetTemplates;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplates;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<GetTemplatesResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(GetTemplatesResponse response)
    {
        response.PageSize.Should().Be(1);
        response.Page.Should().Be(2);
        response.TotalPages.Should().Be(10);
        response.TotalItems.Should().Be(25);
        response.Links.Self.Href.Should().Be(new Uri("https://api.nexmo.com/v2/verify/templates?page=2"));
        response.Links.Next.Href.Should().Be(new Uri("https://api.nexmo.com/v2/verify/templates?page=3"));
        response.Links.Previous.Href.Should().Be(new Uri("https://api.nexmo.com/v2/verify/templates?page=1"));
        response.Links.Last.Href.Should().Be(new Uri("https://api.nexmo.com/v2/verify/templates?page=5"));
        response.Embedded.Templates.Should().HaveCount(2);
        response.Embedded.Templates.Should().BeEquivalentTo(new[]
        {
            new Template(new Guid("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"), "my-template-1", true,
                new TemplateLinks(
                    new HalLink(
                        new Uri("https://api.nexmo.com/v2/verify/templates/8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9")),
                    new HalLink(new Uri(
                        "https://api.nexmo.com/v2/verify/templates/8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9/template_fragments")))),
            new Template(new Guid("0ac50843-b549-4a89-916e-848749f20040"), "my-template-2", false,
                new TemplateLinks(
                    new HalLink(
                        new Uri("https://api.nexmo.com/v2/verify/templates/0ac50843-b549-4a89-916e-848749f20040")),
                    new HalLink(new Uri(
                        "https://api.nexmo.com/v2/verify/templates/0ac50843-b549-4a89-916e-848749f20040/template_fragments")))),
        });
    }
}