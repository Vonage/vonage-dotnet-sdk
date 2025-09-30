#region
using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Serialization;
using Vonage.SubAccounts;
using Vonage.SubAccounts.GetSubAccounts;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.SubAccounts.GetSubAccounts;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    public static Account GetExpectedPrimaryAccount() =>
        new Account(
            "bbe6222f",
            "Department A",
            "bbe6222f",
            true,
            DateTimeOffset.Parse("2018-03-02T16:34:49Z"),
            false,
            (decimal) 100.25,
            (decimal) -100.25
        );

    public static Account[] GetExpectedSubAccounts() => new[]
    {
        new Account(
            "aze1243v",
            "SubAccount department A",
            "bbe6222f",
            false,
            DateTimeOffset.Parse("2018-03-02T17:34:49Z"),
            true,
            (decimal) 1.25,
            15
        ),
    };

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<EmbeddedResponse<GetSubAccountsResponse>>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(success =>
            {
                success.Content.PrimaryAccount.Should().Be(GetExpectedPrimaryAccount());
                success.Content.SubAccounts.Should().BeEquivalentTo(GetExpectedSubAccounts());
            });
}