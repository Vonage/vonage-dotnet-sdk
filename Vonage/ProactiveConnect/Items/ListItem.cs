using System;
using System.Collections.Generic;

namespace Vonage.ProactiveConnect.Items;

/// <summary>
///     Represents a ListItem.
/// </summary>
/// <param name="Id">The unique Id.</param>
/// <param name="CreatedAt">When the list item was created.</param>
/// <param name="UpdatedAt">When the list item was updated.</param>
/// <param name="Data">The custom data.</param>
/// <param name="ListId">The unique list Id.</param>
public record ListItem(Guid Id, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt, Dictionary<string, object> Data,
    Guid ListId);