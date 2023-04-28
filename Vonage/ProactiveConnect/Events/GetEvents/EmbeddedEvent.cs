using System.Collections.Generic;

namespace Vonage.ProactiveConnect.Events.GetEvents;

/// <summary>
///     Represents a wrapper for events.
/// </summary>
/// <param name="Events">The retrieved events.</param>
public record EmbeddedEvents(IEnumerable<BulkEvent> Events);