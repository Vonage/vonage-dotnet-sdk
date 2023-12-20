using System.Collections.Generic;
using EnumsNET;
using FluentAssertions;
using Vonage.Meetings.Common;
using Xunit;

namespace Vonage.Test.Meetings.Common
{
    public class UiSettingsTest
    {
        public static IEnumerable<object[]> GetSupportedLanguages()
        {
            yield return new object[] {UiSettings.UserInterfaceLanguage.PtBr, "pt-br"};
            yield return new object[] {UiSettings.UserInterfaceLanguage.En, "en"};
            yield return new object[] {UiSettings.UserInterfaceLanguage.De, "de"};
            yield return new object[] {UiSettings.UserInterfaceLanguage.Ca, "ca"};
            yield return new object[] {UiSettings.UserInterfaceLanguage.Es, "es"};
            yield return new object[] {UiSettings.UserInterfaceLanguage.Fr, "fr"};
            yield return new object[] {UiSettings.UserInterfaceLanguage.He, "he"};
            yield return new object[] {UiSettings.UserInterfaceLanguage.It, "it"};
            yield return new object[] {UiSettings.UserInterfaceLanguage.ZhCn, "zh-cn"};
            yield return new object[] {UiSettings.UserInterfaceLanguage.ZhTw, "zh-tw"};
        }

        [Theory]
        [MemberData(nameof(GetSupportedLanguages))]
        public void VerifySupportedLanguages(UiSettings.UserInterfaceLanguage language, string expectedLanguage) =>
            language.AsString(EnumFormat.Description).Should().Be(expectedLanguage);
    }
}