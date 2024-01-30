using FluentAssertions;
using Vonage.Common.Serialization;
using Xunit;

namespace Vonage.Test.Common.Serialization;

[Trait("Category", "Serialization")]
public class EnumDescriptionJsonConverterTest
{
    private readonly EnumDescriptionJsonConverter<DummyEnum> converter = new EnumDescriptionJsonConverter<DummyEnum>();

    [Fact]
    public void HandleNull_ShouldReturnTrue() => this.converter.HandleNull.Should().BeTrue();

    private enum DummyEnum
    {
    }
}