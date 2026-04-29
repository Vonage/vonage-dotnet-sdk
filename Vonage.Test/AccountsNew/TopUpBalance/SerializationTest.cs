using FluentAssertions;
using Vonage.AccountsNew.TopUpBalance;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.AccountsNew.TopUpBalance;

[Trait("Category", "Serialization")]
[Trait("Product", "AccountsNew")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldSerialize() =>
        TopUpBalanceRequest.Build()
            .WithTransactionReference("8ef2447e69604f642ae59363aa5f781b")
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<TopUpBalanceResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(response =>
            {
                response.ErrorCode.Should().Be("200");
                response.ErrorCodeLabel.Should().Be("success");
            });
}
