using Vonage.AccountsNew.RevokeSecret;
using Vonage.Serialization;
using Vonage.Test.Common;
using Xunit;

namespace Vonage.Test.AccountsNew.RevokeSecret;

[Trait("Category", "Serialization")]
[Trait("Product", "AccountsNew")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());
}
