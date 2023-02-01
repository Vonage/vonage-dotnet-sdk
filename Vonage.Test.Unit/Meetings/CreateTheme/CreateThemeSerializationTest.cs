using System;
using System.Drawing;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.CreateTheme;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateTheme
{
    public class CreateThemeSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public CreateThemeSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(CreateThemeSerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldSerialize() =>
            CreateThemeVonageRequestBuilder
                .Build("Brand", Color.FromName("#FF00FF"))
                .WithName("Theme1")
                .WithShortCompanyUrl(new Uri("https://example.com"))
                .Create()
                .Map(value => value.BuildRequestMessage())
                .Map(value => value.Content.ReadAsStringAsync().Result)
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithDefaultValues() =>
            CreateThemeVonageRequestBuilder
                .Build("Brand", Color.FromName("#FF00FF"))
                .Create()
                .Map(value => value.BuildRequestMessage())
                .Map(value => value.Content.ReadAsStringAsync().Result)
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}