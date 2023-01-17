namespace Vonage.Meetings.GetAvailableRooms;

/// <summary>
///     Represents a request to retrieve all available rooms.
/// </summary>
public readonly struct GetAvailableRoomsRequest
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="startId">The ID to start returning events at.</param>
    /// <param name="endId">The ID to end returning events at (excluding end_id itself).</param>
    private GetAvailableRoomsRequest(string startId, string endId)
    {
        this.StartId = startId;
        this.EndId = endId;
    }

    /// <summary>
    ///     The ID to end returning events at (excluding end_id itself).
    /// </summary>
    public string EndId { get; }

    /// <summary>
    ///     The ID to start returning events at.
    /// </summary>
    public string StartId { get; }

    /// <summary>
    ///     Build the request with default values.
    /// </summary>
    /// <returns>The request.</returns>
    public static GetAvailableRoomsRequest Build() => new(null, null);

    /// <summary>
    ///     Build the request with the specified values.
    /// </summary>
    /// <param name="startId">The ID to start returning events at.</param>
    /// <param name="endId">The ID to end returning events at (excluding end_id itself).</param>
    /// <returns>The request</returns>
    public static GetAvailableRoomsRequest Build(string startId, string endId) => new(startId, endId);
}