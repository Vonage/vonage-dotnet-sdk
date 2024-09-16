#region
using System;
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2;
using Vonage.VerifyV2.StartVerification;
using Xunit;
#endregion

namespace Vonage.Test.VerifyV2.GetTemplateFragment;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<TemplateFragment>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(TemplateFragment response) =>
        response.Should().Be(new TemplateFragment(
            new Guid("c70f446e-997a-4313-a081-60a02a31dc19"),
            VerificationChannel.Sms,
            Locale.EnUs,
            "Text content of the template. May contain 4 reserved variables: `${code}`, `${brand}`, `${time-limit}` and `${time-limit-unit}`",
            DateTimeOffset.Parse("2021-08-30T20:12:15.178Z"),
            DateTimeOffset.Parse("2023-08-30T15:20:15.178Z")));
}