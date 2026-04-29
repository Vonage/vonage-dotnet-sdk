using System;
using FluentAssertions;
using Vonage.AccountsNew;
using Vonage.Common;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.AccountsNew;

[Trait("Category", "Serialization")]
[Trait("Product", "AccountsNew")]
public class SecretInfoSerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SecretInfoSerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    internal static SecretInfo GetExpectedSecretInfo() =>
        new SecretInfo(
            new HalLinks
            {
                Self = new HalLink(new Uri(
                    "https://api.vonage.com/v1/accounts/abcd1234/secrets/ad6dc56f-07b5-46e1-a527-85530e625800")),
            },
            "ad6dc56f-07b5-46e1-a527-85530e625800",
            DateTimeOffset.Parse("2017-03-02T16:34:49Z"));

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<SecretInfo>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(GetExpectedSecretInfo());
}
