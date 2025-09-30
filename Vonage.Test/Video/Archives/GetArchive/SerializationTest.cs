#region
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives;
using Xunit;
#endregion

namespace Vonage.Test.Video.Archives.GetArchive;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<Archive>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(ArchiveTest.VerifyArchive);
}