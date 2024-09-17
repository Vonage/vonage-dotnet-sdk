#region
using System;
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2;
using Vonage.VerifyV2.GetTemplateFragments;
using Vonage.VerifyV2.StartVerification;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplateFragments;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<GetTemplateFragmentsResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(GetTemplateFragmentsResponse response)
    {
        response.PageSize.Should().Be(1);
        response.Page.Should().Be(2);
        response.TotalPages.Should().Be(10);
        response.TotalItems.Should().Be(25);
        response.Links.Self.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v2/verify/templates/c70f446e-997a-4313-a081-60a02a31dc19/template_fragments?page=2"));
        response.Links.Next.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v2/verify/templates/c70f446e-997a-4313-a081-60a02a31dc19/template_fragments?page=3"));
        response.Links.Previous.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v2/verify/templates/c70f446e-997a-4313-a081-60a02a31dc19/template_fragments?page=1"));
        response.Links.Last.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v2/verify/templates/c70f446e-997a-4313-a081-60a02a31dc19/template_fragments?page=5"));
        response.Embedded.Fragments.Should().HaveCount(1);
        response.Embedded.Fragments.Should().BeEquivalentTo(new[]
        {
            new TemplateFragment(
                new Guid("c70f446e-997a-4313-a081-60a02a31dc19"),
                VerificationChannel.Sms,
                Locale.EnUs,
                "Text content of the template. May contain 4 reserved variables: `${code}`, `${brand}`, `${time-limit}` and `${time-limit-unit}`",
                DateTimeOffset.Parse("2021-08-30T20:12:15.178Z"),
                DateTimeOffset.Parse("2023-08-30T15:20:15.178Z")),
        });
    }
}