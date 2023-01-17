namespace Vonage.Common.Monads;

/// <summary>
///     A unit type is a type that allows only one value (and thus can hold no information)
/// </summary>
public readonly struct Unit
{
    /// <summary>
    ///     The default Unit.
    /// </summary>
    public static readonly Unit Default = new();
}