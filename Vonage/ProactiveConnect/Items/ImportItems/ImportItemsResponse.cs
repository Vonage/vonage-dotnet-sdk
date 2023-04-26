namespace Vonage.ProactiveConnect.Items.ImportItems;

/// <summary>
///     Represents the response when importing items.
/// </summary>
/// <param name="Inserted">The number of inserted items.</param>
/// <param name="Updated">The number of updated items.</param>
/// <param name="Deleted">The number of deleted items.</param>
public record ImportItemsResponse(int? Inserted, int? Updated, int? Deleted);