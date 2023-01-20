namespace Vonage.Meetings.GetDialNumbers;

/// <summary>
/// </summary>
public struct GetDialNumbersResponse
{
    /// <summary>
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// </summary>
    public string Locale { get; set; }

    /// <summary>
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    ///     Constructors.
    /// </summary>
    /// <param name="number"></param>
    /// <param name="locale"></param>
    /// <param name="displayName"></param>
    public GetDialNumbersResponse(string number, string locale, string displayName)
    {
        this.Number = number;
        this.Locale = locale;
        this.DisplayName = displayName;
    }
}