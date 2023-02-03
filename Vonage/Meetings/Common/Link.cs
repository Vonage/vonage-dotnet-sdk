using System;

namespace Vonage.Meetings.Common;

/// <summary>
///     Represents a link to another page.
/// </summary>
public struct Link
{
    /// <summary>
    ///     Hypertext Reference.
    /// </summary>
    public Uri Href { get; set; }
}