using System;

namespace Vonage.Common.Client.Builders;

/// <summary>
///     Represents a builder for ApplicationId.
/// </summary>
/// <typeparam name="T">Type of the underlying request.</typeparam>
public interface IBuilderForApplicationId<T> where T : IVonageRequest
{
    /// <summary>
    ///     Sets the ApplicationId.
    /// </summary>
    /// <param name="value">The ApplicationId.</param>
    /// <returns>The builder.</returns>
    IBuilderForArchiveId<T> WithApplicationId(Guid value);
}