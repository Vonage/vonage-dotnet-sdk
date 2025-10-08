#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs.Suggestions;

/// <summary>
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(DialSuggestion), "dial")]
[JsonDerivedType(typeof(ReplySuggestion), "reply")]
public abstract record SuggestionBase
{
    /// <summary>
    ///     The type for the suggestion object.
    /// </summary>
    public abstract string Type { get; }
}