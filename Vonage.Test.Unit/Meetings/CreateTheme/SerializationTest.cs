using System;
using System.Drawing;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateTheme
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

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
                    success.ApplicationId.Should().Be(new Guid("a98e12ca-f3e5-4df8-bc66-fd4b5f30b9e9"));
                    success.ThemeId.Should().Be(new Guid("cf7f7327-c8f3-4575-b113-0598571b499a"));
                    success.BrandImageColored.Should().Be("abc123");
                    success.BrandImageColoredUrl.Should().Be(new Uri("https://example.com"));
                    success.BrandImageWhite.Should().Be("abc123");
                    success.BrandImageWhiteUrl.Should().Be(new Uri("https://example.com"));
                    success.BrandedFavicon.Should().Be("abc123");
                    success.BrandedFaviconUrl.Should().Be(new Uri("https://example.com"));
                    success.MainColor.Should().Be(Color.FromArgb(255, 255, 0, 255));
                    success.ShortCompanyUrl.Should().Be(new Uri("https://example.com"));
                    success.BrandText.Should().Be("Brand");
                });

        [Fact]
        public void ShouldSerialize() =>
            CreateThemeRequest
                .Build()
                .WithBrand("Brand")
                .WithColor(Color.FromArgb(255, 255, 0, 255))
                .WithName("Theme1")
                .WithShortCompanyUrl(new Uri("https://example.com"))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithDefaultValues() =>
            CreateThemeRequest
                .Build()
                .WithBrand("Brand")
                .WithColor(Color.FromArgb(255, 255, 0, 255))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}