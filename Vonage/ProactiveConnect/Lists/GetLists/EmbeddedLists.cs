using System.Collections.Generic;

namespace Vonage.ProactiveConnect.Lists.GetLists;

/// <summary>
///     Represents a wrapper for lists.
/// </summary>
/// <param name="Lists">The retrieved lists.</param>
public record EmbeddedLists(IEnumerable<List> Lists);