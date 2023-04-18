using System;

namespace Vonage.Common.Client.Builders;

/// <summary>
///     Represents a builder for ArchiveId.
/// </summary>
/// <typeparam name="T">Type of the underlying request.</typeparam>
public interface IBuilderForArchiveId<T> where T : IVonageRequest
{
    /// <summary>
    ///     Sets the ArchiveId.
    /// </summary>
    /// <param name="value">The ArchiveId.</param>
    /// <returns>The builder.</returns>
    IBuilderForStreamId<T> WithArchiveId(Guid value);
}