using FluentAssertions;
using Vonage.Video.Beta.Video.Archives.Common;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Archives.Common
{
    public class RenderResolutionConverterTest
    {
        private readonly RenderResolutionConverter converter;

        public RenderResolutionConverterTest()
        {
            this.converter = new RenderResolutionConverter();
        }

        [Fact]
        public void HandleNull_ShouldReturnTrue() => this.converter.HandleNull.Should().BeTrue();
    }
}