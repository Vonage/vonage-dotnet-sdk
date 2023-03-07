namespace Vonage.Messages.Viber;

/// <summary>
///     Represents information for Viber action buttons.
/// </summary>
/// <param name="Url">A URL which is requested when the action button is clicked.</param>
/// <param name="Text">Text which is rendered on the action button.</param>
public record ViberAction(string Url, string Text);