using Vonage.Common.Monads;

namespace Vonage.Common.Client;

/// <summary>
///     Represents a builder for the underlying request type.
/// </summary>
/// <typeparam name="T">The request type.</typeparam>
public interface IRequestBuilder<T> where T : IVonageRequest
{
    /// <summary>
    ///     Creates a request.
    /// </summary>
    /// <returns>The request if validation succeeded, a failure if it failed.</returns>
    Result<T> Create();
}