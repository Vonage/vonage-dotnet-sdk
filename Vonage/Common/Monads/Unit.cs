namespace Vonage.Common.Monads;

/// <summary>
///     A unit type that allows only one value and carries no information.
/// </summary>
/// <remarks>
///     <para>Unit is used as a return type when you need to indicate completion but have no value to return,
///     similar to void but usable in generic contexts.</para>
///     <para>Commonly used with <see cref="Result{T}"/> when an operation succeeds but has no return value.</para>
/// </remarks>
/// <example>
/// <code><![CDATA[
/// // Use Unit for operations that succeed without returning a value
/// Result<Unit> result = await client.DeleteRecordingAsync(recordingId);
/// result.Match(
///     success => Console.WriteLine("Deleted successfully"),
///     failure => Console.WriteLine($"Failed: {failure.GetFailureMessage()}")
/// );
/// ]]></code>
/// </example>
public readonly struct Unit
{
    /// <summary>
    ///     The single Unit value. Use this when returning Unit.
    /// </summary>
    public static readonly Unit Default = new();
}