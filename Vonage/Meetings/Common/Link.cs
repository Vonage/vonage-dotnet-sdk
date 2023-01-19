namespace Vonage.Meetings.Common;

/// <summary>
///     Represents a link to another page.
/// </summary>
public struct Link
{
    /// <summary>
    ///     Hypertext Reference.
    /// </summary>
    public string Href { get; set; }

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="href"> Hypertext Reference.</param>
    public Link(string href) => this.Href = href;
}