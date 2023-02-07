using System;
using System.Drawing;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetThemes
{
    public class GetThemesSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetThemesSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetThemesSerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

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
                    success[0].ApplicationId.Should().Be(new Guid("a98e12ca-f3e5-4df8-bc66-fd4b5f30b9e9"));
                    success[0].ThemeId.Should().Be(new Guid("cf7f7327-c8f3-4575-b113-0598571b499a"));
                    success[0].BrandImageColored.Should().Be("abc123");
                    success[0].BrandImageColoredUrl.Should().Be(new Uri("https://example.com"));
                    success[0].BrandImageWhite.Should().Be("abc123");
                    success[0].BrandImageWhiteUrl.Should().Be(new Uri("https://example.com"));
                    success[0].BrandedFavicon.Should().Be("abc123");
                    success[0].BrandedFaviconUrl.Should().Be(new Uri("https://example.com"));
                    success[0].MainColor.Should().Be(Color.FromArgb(255, 255, 0, 255));
                    success[0].ShortCompanyUrl.Should().Be(new Uri("https://example.com"));
                    success[0].BrandText.Should().Be("Brand");
                });
    }
}