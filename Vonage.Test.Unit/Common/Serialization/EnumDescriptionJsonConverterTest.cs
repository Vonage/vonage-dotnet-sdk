using FluentAssertions;
using Vonage.Common.Serialization;

namespace Vonage.Common.Test.Serialization
{
    public class EnumDescriptionJsonConverterTest
    {
        private readonly EnumDescriptionJsonConverter<DummyEnum> converter;

        public EnumDescriptionJsonConverterTest()
        {
            this.converter = new EnumDescriptionJsonConverter<DummyEnum>();
        }

        [Fact]
        public void HandleNull_ShouldReturnTrue() => this.converter.HandleNull.Should().BeTrue();

        private enum DummyEnum
        {
        }
    }
}