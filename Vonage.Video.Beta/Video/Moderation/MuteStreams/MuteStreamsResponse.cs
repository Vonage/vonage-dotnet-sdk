namespace Vonage.Video.Beta.Video.Moderation.MuteStreams;

/// <summary>
///     Represents the response when streams have been muted.
/// </summary>
public class MuteStreamsResponse
{
    /// <summary>
    ///     The Vonage application ID.
    /// </summary>
    public string ApplicationId { get; set; }

    /// <summary>
    ///     Whether the project is active ("ACTIVE") or suspended ("SUSPENDED").
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    ///     The name, if you specified one when creating the project; or an empty string if you did not specify a name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     This is set to "standard" or "enterprise", and it refers to the environment a project is running on. Enterprise
    ///     package partners have access to the enterprise environment.
    /// </summary>
    public string Environment { get; set; }

    /// <summary>
    ///     The time at which the project was created (a UNIX timestamp, in milliseconds).
    /// </summary>
    public int CreatedAt { get; set; }

    /// <summary>
    ///     Creates a response.
    /// </summary>
    /// <param name="applicationId">  The Vonage application ID.</param>
    /// <param name="status"> Whether the project is active ("ACTIVE") or suspended ("SUSPENDED").</param>
    /// <param name="name">
    ///     The name, if you specified one when creating the project; or an empty string if you did not specify
    ///     a name.
    /// </param>
    /// <param name="environment">
    ///     This is set to "standard" or "enterprise", and it refers to the environment a project is
    ///     running on. Enterprise package partners have access to the enterprise environment.
    /// </param>
    /// <param name="createdAt">   The time at which the project was created (a UNIX timestamp, in milliseconds).</param>
    public MuteStreamsResponse(string applicationId, string status, string name, string environment, int createdAt)
    {
        this.ApplicationId = applicationId;
        this.Status = status;
        this.Name = name;
        this.Environment = environment;
        this.CreatedAt = createdAt;
    }
}