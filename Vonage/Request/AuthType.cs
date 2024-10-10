namespace Vonage.Request;

/// <summary>
///     Defines the authentication type.
/// </summary>
public enum AuthType
{
    /// <summary>
    ///     Basic authentication.
    /// </summary>
    Basic,
    
    /// <summary>
    ///     Bearer authentication.
    /// </summary>
    Bearer,
    
    /// <summary>
    ///     Query authentication.
    /// </summary>
    Query,
}