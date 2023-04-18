using System;

namespace Vonage.Common.Client.Builders;

/// <summary>
///     Represents a builder for StreamId.
/// </summary>
/// <typeparam name="T">Type of the underlying request.</typeparam>
public interface IBuilderForStreamId<T> where T : IVonageRequest
{
    /// <summary>
    ///     Sets the StreamId.
    /// </summary>
    /// <param name="value">The StreamId.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<T> WithStreamId(Guid value);
}