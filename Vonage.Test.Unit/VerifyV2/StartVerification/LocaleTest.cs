using System.Collections.Generic;
using FluentAssertions;
using Vonage.VerifyV2.StartVerification;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification
{
    public class LocaleTest
    {
        public static IEnumerable<object[]> GetSupportedLocales()
        {
            yield return new object[] {Locale.EnUs, "en-us"};
            yield return new object[] {Locale.EnGb, "en-gb"};
            yield return new object[] {Locale.EsEs, "es-es"};
            yield return new object[] {Locale.EsMx, "es-mx"};
            yield return new object[] {Locale.EsUs, "es-us"};
            yield return new object[] {Locale.ItIt, "it-it"};
            yield return new object[] {Locale.FrFr, "fr-fr"};
            yield return new object[] {Locale.DeDe, "de-de"};
            yield return new object[] {Locale.RuRu, "ru-ru"};
            yield return new object[] {Locale.HiIn, "hi-in"};
            yield return new object[] {Locale.PtBr, "pt-br"};
            yield return new object[] {Locale.PtPt, "pt-pt"};
            yield return new object[] {Locale.IdId, "id-id"};
            yield return new object[] {Locale.JaJp, "ja-jp"};
        }

        [Theory]
        [MemberData(nameof(GetSupportedLocales))]
        public void VerifySupportedLocales(Locale locale, string expectedLanguage) =>
            locale.Language.Should().Be(expectedLanguage);
    }
}