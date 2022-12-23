namespace Vonage.Video.Beta.Video.Archives.GetArchives;

/// <summary>
///     Represents the response for retrieving archives.
/// </summary>
public struct GetArchivesResponse
{
    /// <summary>
    ///     The total number of archives for the API key.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    ///     An array of objects defining each archive retrieved. Archives are listed from the newest to the oldest in the
    ///     return set.
    /// </summary>
    /// r
    public Archive[] Items { get; set; }

    /// <summary>
    ///     Creates a response.
    /// </summary>
    /// <param name="count">The number of elements.</param>
    /// <param name="items">The streams.</param>
    public GetArchivesResponse(int count, Archive[] items)
    {
        this.Count = count;
        this.Items = items;
    }
}