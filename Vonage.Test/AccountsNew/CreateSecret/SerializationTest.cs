using Vonage.AccountsNew.CreateSecret;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.AccountsNew.CreateSecret;

[Trait("Category", "Serialization")]
[Trait("Product", "AccountsNew")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    internal static Result<CreateSecretRequest> BuildRequest() =>
        CreateSecretRequest.Build()
            .WithApiKey("abcd1234")
            .WithSecret("example-4PI-s3cret")
            .Create();

    [Fact]
    public void ShouldSerialize() =>
        BuildRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}
