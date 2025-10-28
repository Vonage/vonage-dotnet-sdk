#region
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions.ListConnections;
using Xunit;
#endregion

namespace Vonage.Test.Video.Sessions.ListConnections;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<ListConnectionsResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyConnections);

    internal static void VerifyConnections(ListConnectionsResponse success)
    {
        success.Count.Should().Be(1);
        success.ApplicationId.Should().Be("e9f8c166-6c67-440d-994a-04fb6dfed007");
        success.SessionId.Should().Be("b40ef09b-3811-4726-b508-e41a0f96c68f");
        success.Items.Length.Should().Be(1);
        success.Items[0].Should().Be(new Connection("string", Connection.State.Connected, 1384221730000));
    }
}