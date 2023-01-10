using FluentAssertions;
using Vonage.Video.Beta.Common.Serialization;
using Vonage.Video.Beta.Video.Archives.Common;
using Xunit;

namespace Vonage.Video.Beta.Test.Common.Serialization
{
    public class EnumDescriptionJsonConverterTest
    {
        private readonly EnumDescriptionJsonConverter<LayoutType> converter;

        public EnumDescriptionJsonConverterTest()
        {
            this.converter = new EnumDescriptionJsonConverter<LayoutType>();
        }

        [Fact]
        public void HandleNull_ShouldReturnTrue() => this.converter.HandleNull.Should().BeTrue();
    }
}