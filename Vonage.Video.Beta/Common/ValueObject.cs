using System.Collections.Generic;
using System.Linq;

namespace Vonage.Video.Beta.Common;

/// <summary>
///     Represents a ValueObject with value equality comparison.
/// </summary>
/// <typeparam name="T">Type of the value.</typeparam>
public abstract class ValueObject<T>
{
    /// <inheritdoc />
    public override bool Equals(object obj) => obj is ValueObject<T> other && this.EqualsValueObject(other);

    /// <inheritdoc />
    public override int GetHashCode() =>
        this.GetEqualityComponents()
            .Select(item => item?.GetHashCode() ?? 0)
            .Aggregate(1, (hash, element) => hash * 23 + element);

    /// <summary>
    ///     Override for equality operator.
    /// </summary>
    /// <param name="first">A ValueObject.</param>
    /// <param name="second">A ValueObject.</param>
    /// <returns>If both ValueObjects are equal.</returns>
    public static bool operator ==(ValueObject<T> first, ValueObject<T> second) =>
        first?.EqualsValueObject(second) ?? second is null;

    /// <summary>
    ///     Override for difference operator.
    /// </summary>
    /// <param name="first">A ValueObject.</param>
    /// <param name="second">A ValueObject.</param>
    /// <returns>If both ValueObjects are different.</returns>
    public static bool operator !=(ValueObject<T> first, ValueObject<T> second) => !(first == second);

    private bool EqualsValueObject(ValueObject<T> other) =>
        this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

    /// <summary>
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<object> GetEqualityComponents();
}