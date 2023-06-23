using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;

namespace Vonage.Meetings.Common;

/// <summary>
///     Provides options to customize the user interface
/// </summary>
/// <param name="Language">The desired language of the UI.</param>
public record UiSettings(
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<UiSettings.UserInterfaceLanguage>))]
    UiSettings.UserInterfaceLanguage Language)
{
    /// <summary>
    ///     The desired language of the UI.
    /// </summary>
    public enum UserInterfaceLanguage
    {
        /// <summary>
        ///     English language.
        /// </summary>
        [Description("en")] En,

        /// <summary>
        ///     Hebrew language.
        /// </summary>
        [Description("he")] He,

        /// <summary>
        ///     Spanish language.
        /// </summary>
        [Description("es")] Es,

        /// <summary>
        ///     Portuguese language.
        /// </summary>
        [Description("pt")] Pt,

        /// <summary>
        ///     Italian language.
        /// </summary>
        [Description("it")] It,

        /// <summary>
        ///     Canadian language.
        /// </summary>
        [Description("ca")] Ca,

        /// <summary>
        ///     French language.
        /// </summary>
        [Description("fr")] Fr,

        /// <summary>
        ///     German language.
        /// </summary>
        [Description("de")] De,
    }
}