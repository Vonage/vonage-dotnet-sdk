using FluentAssertions;
using Vonage.Video.Beta.Video.Archives.Common;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Archives.Common
{
    public class LayoutTypeConverterTest
    {
        private readonly LayoutTypeConverter converter;

        public LayoutTypeConverterTest()
        {
            this.converter = new LayoutTypeConverter();
        }

        [Fact]
        public void HandleNull_ShouldReturnTrue() => this.converter.HandleNull.Should().BeTrue();
    }
}