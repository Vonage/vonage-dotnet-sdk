using System.Collections.Generic;

namespace Vonage.ProactiveConnect.Items.GetItems;

/// <summary>
///     Represents a wrapper for items.
/// </summary>
/// <param name="Items">The retrieved items.</param>
public record EmbeddedItems(IEnumerable<ListItem> Items);