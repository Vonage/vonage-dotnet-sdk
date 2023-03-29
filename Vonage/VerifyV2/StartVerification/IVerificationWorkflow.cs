namespace Vonage.VerifyV2.StartVerification;

/// <summary>
///     Represents a verification workflow.
/// </summary>
/// <remarks>
///     This is a marker interface.
/// </remarks>
public interface IVerificationWorkflow
{
    /// <summary>
    ///     The verification channel.
    /// </summary>
    string Channel { get; }
}