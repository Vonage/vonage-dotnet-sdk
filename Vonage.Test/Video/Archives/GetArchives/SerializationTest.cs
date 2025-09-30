#region
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.GetArchives;
using Xunit;
#endregion

namespace Vonage.Test.Video.Archives.GetArchives;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<GetArchivesResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyArchives);

    internal static void VerifyArchives(GetArchivesResponse success)
    {
        success.Count.Should().Be(1);
        success.Items.Length.Should().Be(1);
        ArchiveTest.VerifyArchive(success.Items[0]);
    }
}