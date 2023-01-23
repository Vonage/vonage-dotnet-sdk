using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetApplicationThemes;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetApplicationThemes
{
    public class GetApplicationThemesDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetApplicationThemesDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetApplicationThemesDeserializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<Theme[]>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Length.Should().Be(1);
                    success[0].Domain.Should().Be(ThemeDomain.VBC);
                    success[0].ThemeName.Should().Be("Theme1");
                    success[0].AccountId.Should().Be("abc123");
                    success[0].ApplicationId.Should().Be("abc123");
                    success[0].ThemeId.Should().Be("abc123");
                    success[0].BrandImageColored.Should().Be("abc123");
                    success[0].BrandImageColoredUrl.Should().Be("abc123");
                    success[0].BrandImageWhite.Should().Be("abc123");
                    success[0].BrandImageWhiteUrl.Should().Be("abc123");
                    success[0].BrandedFavicon.Should().Be("abc123");
                    success[0].BrandedFaviconUrl.Should().Be("abc123");
                    success[0].MainColor.Should().Be("#12f64e");
                    success[0].ShortCompanyUrl.Should().Be("short-url");
                    success[0].BrandText.Should().Be("Brand");
                });
    }
}