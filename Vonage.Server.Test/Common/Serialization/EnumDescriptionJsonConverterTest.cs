using FluentAssertions;
using Vonage.Server.Common.Serialization;
using Vonage.Server.Video.Archives.Common;
using Xunit;

namespace Vonage.Server.Test.Common.Serialization
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