using System;
using System.Text.Json.Serialization;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;

namespace Vonage.Common.Failures;

/// <inheritdoc />
public readonly struct DeserializationFailure : IResultFailure
{
    /// <summary>
    ///     The expected type for deserialization.
    /// </summary>
    public Type ExpectedType { get; }

    /// <summary>
    ///     The serialized content.
    /// </summary>
    public string SerializedContent { get; }

    /// <inheritdoc />
    public Type Type => typeof(DeserializationFailure);

    /// <summary>
    ///     Creates a DeserializationFailure.
    /// </summary>
    /// <param name="expectedType">expectedType</param>
    /// <param name="serializedContent">serializedContent</param>
    [JsonConstructor]
    public DeserializationFailure(Type expectedType, string serializedContent)
    {
        this.ExpectedType = expectedType;
        this.SerializedContent = serializedContent;
    }

    /// <summary>
    ///     Creates a DeserializationFailure.
    /// </summary>
    /// <param name="expectedType">expectedType</param>
    /// <param name="serializedContent">serializedContent</param>
    /// <returns>The failure.</returns>
    public static DeserializationFailure From(Type expectedType, string serializedContent) =>
        new(expectedType, serializedContent);

    /// <inheritdoc />
    public string GetFailureMessage() =>
        $"Unable to deserialize '{this.SerializedContent}' into '{this.ExpectedType.Name}'.";

    /// <inheritdoc />
    public Exception ToException() => new VonageHttpRequestException(this.GetFailureMessage())
    {
        Json = this.SerializedContent,
    };

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);
}