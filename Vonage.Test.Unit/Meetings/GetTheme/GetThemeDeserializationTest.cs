using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetTheme
{
    public class GetThemeDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetThemeDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetThemeDeserializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<Theme>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Domain.Should().Be(ThemeDomain.VBC);
                    success.ThemeName.Should().Be("Theme1");
                    success.AccountId.Should().Be("abc123");
                    success.ApplicationId.Should().Be("abc123");
                    success.ThemeId.Should().Be("abc123");
                    success.BrandImageColored.Should().Be("abc123");
                    success.BrandImageColoredUrl.Should().Be("abc123");
                    success.BrandImageWhite.Should().Be("abc123");
                    success.BrandImageWhiteUrl.Should().Be("abc123");
                    success.BrandedFavicon.Should().Be("abc123");
                    success.BrandedFaviconUrl.Should().Be("abc123");
                    success.MainColor.Should().Be("#12f64e");
                    success.ShortCompanyUrl.Should().Be("short-url");
                    success.BrandText.Should().Be("Brand");
                });
    }
}