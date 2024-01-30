using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions.CreateSession;
using Xunit;

namespace Vonage.Test.Video.Sessions.CreateSession;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper;

    public SerializationTest() =>
        this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithCamelCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer.DeserializeObject<CreateSessionResponse[]>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifySessions);

    [Fact]
    public void ShouldDeserialize200_GivenEmptyArray() =>
        this.helper.Serializer.DeserializeObject<CreateSessionResponse[]>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(value => value.Should().BeEmpty());

    internal static void VerifySessions(CreateSessionResponse[] content)
    {
        content.Length.Should().Be(1);
        content[0].SessionId.Should()
            .Be(
                "2_MX5hOThlMTJjYS1mM2U1LTRkZjgtYmM2Ni1mZDRiNWYzMGI5ZTl-fjE2NzI3MzY4NzgxNjJ-bi9OeFVLbkNaVjBUUnpVSmxjbURqQ3J4flB-fg");
    }
}