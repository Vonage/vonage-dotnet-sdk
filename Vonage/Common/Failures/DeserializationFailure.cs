#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Failures;

/// <inheritdoc />
/// <example>
///     <code><![CDATA[
/// // Create a deserialization failure when JSON doesn't match expected type
/// string invalidJson = "{\"wrong\":\"format\"}";
/// DeserializationFailure failure = DeserializationFailure.From(typeof(User), invalidJson);
/// Console.WriteLine(failure.GetFailureMessage());
/// // "Unable to deserialize '{"wrong":"format"}' into 'User'."
/// ]]></code>
/// </example>
public readonly struct DeserializationFailure : IResultFailure
{
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
    ///     The expected type for deserialization.
    /// </summary>
    public Type ExpectedType { get; }

    /// <summary>
    ///     The serialized content.
    /// </summary>
    public string SerializedContent { get; }

    /// <inheritdoc />
    public Type Type => typeof(DeserializationFailure);

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

    /// <summary>
    ///     Creates a DeserializationFailure.
    /// </summary>
    /// <param name="expectedType">expectedType</param>
    /// <param name="serializedContent">serializedContent</param>
    /// <returns>The failure.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// DeserializationFailure failure = DeserializationFailure.From(typeof(Customer), "{\"id\":123}");
    /// Result<Customer> result = failure.ToResult<Customer>();
    /// ]]></code>
    /// </example>
    public static DeserializationFailure From(Type expectedType, string serializedContent) =>
        new DeserializationFailure(expectedType, serializedContent);
}