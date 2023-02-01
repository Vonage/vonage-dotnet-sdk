using System;

namespace Vonage.Meetings.UpdateApplication;

/// <summary>
///     Represents a response when updating an application.
/// </summary>
public struct UpdateApplicationResponse
{
    /// <summary>
    ///     The applications account id.
    /// </summary>
    public string AccountId { get; set; }

    /// <summary>
    ///     The application id.
    /// </summary>
    public Guid ApplicationId { get; set; }

    /// <summary>
    ///     The application default theme id.
    /// </summary>
    public Guid DefaultThemeId { get; set; }
}