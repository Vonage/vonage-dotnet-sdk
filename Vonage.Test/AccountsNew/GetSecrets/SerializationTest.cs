using System;
using FluentAssertions;
using Vonage.AccountsNew;
using Vonage.AccountsNew.GetSecrets;
using Vonage.Common;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.AccountsNew.GetSecrets;

[Trait("Category", "Serialization")]
[Trait("Product", "AccountsNew")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<GetSecretsResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(response =>
            {
                response.Links.Self.Href.Should().Be(
                    new Uri("https://api.vonage.com/v1/accounts/abcd1234/secrets"));
                response.Embedded.Secrets.Should().HaveCount(1);
                response.Embedded.Secrets[0].Id.Should().Be("ad6dc56f-07b5-46e1-a527-85530e625800");
                response.Embedded.Secrets[0].CreatedAt.Should().Be(DateTimeOffset.Parse("2017-03-02T16:34:49Z"));
            });
}
