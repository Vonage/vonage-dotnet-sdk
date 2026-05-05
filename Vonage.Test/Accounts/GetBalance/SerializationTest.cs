using FluentAssertions;
using Vonage.Accounts.GetBalance;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Accounts.GetBalance;

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
            .DeserializeObject<GetBalanceResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(response =>
            {
                response.Value.Should().Be(10.28m);
                response.AutoReload.Should().BeFalse();
            });
}
