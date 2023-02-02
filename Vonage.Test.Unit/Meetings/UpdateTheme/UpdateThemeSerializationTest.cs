using System;
using System.Drawing;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.UpdateTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateTheme
{
    public class UpdateThemeSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public UpdateThemeSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(UpdateThemeSerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldSerialize() =>
            UpdateThemeRequestBuilder
                .Build("ThemeId")
                .WithColor(Color.FromName("#FF00FF"))
                .WithName("Theme1")
                .WithBrandText("Brand")
                .WithShortCompanyUrl(new Uri("https://example.com"))
                .Create()
                .Map(value => value.BuildRequestMessage())
                .Map(value => value.Content.ReadAsStringAsync().Result)
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeEmpty() =>
            UpdateThemeRequestBuilder
                .Build("ThemeId")
                .Create()
                .Map(value => value.BuildRequestMessage())
                .Map(value => value.Content.ReadAsStringAsync().Result)
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}