#region
using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplate;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<Template>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(Template response) =>
        response.Should().Be(new Template(new Guid("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"), "my-template", true,
            new TemplateLinks(
                new HalLink(new Uri("https://api.nexmo.com/v2/verify/templates/8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9")),
                new HalLink(new Uri(
                    "https://api.nexmo.com/v2/verify/templates/8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9/template_fragments")))));
}