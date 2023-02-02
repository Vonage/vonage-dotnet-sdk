﻿using System;
using System.Drawing;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateTheme
{
    public class UpdateThemeDeserializationTest
    {
        private readonly SerializationTestHelper helper;

        public UpdateThemeDeserializationTest() =>
            this.helper = new SerializationTestHelper(typeof(UpdateThemeDeserializationTest).Namespace,
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
                    success.ApplicationId.Should().Be("abc123");
                    success.ThemeId.Should().Be("abc123");
                    success.BrandImageColored.Should().Be("abc123");
                    success.BrandImageColoredUrl.Should().Be(new Uri("https://example.com"));
                    success.BrandImageWhite.Should().Be("abc123");
                    success.BrandImageWhiteUrl.Should().Be(new Uri("https://example.com"));
                    success.BrandedFavicon.Should().Be("abc123");
                    success.BrandedFaviconUrl.Should().Be(new Uri("https://example.com"));
                    success.MainColor.Should().Be(Color.FromName("#FF00FF"));
                    success.ShortCompanyUrl.Should().Be(new Uri("https://example.com"));
                    success.BrandText.Should().Be("Brand");
                });
    }
}