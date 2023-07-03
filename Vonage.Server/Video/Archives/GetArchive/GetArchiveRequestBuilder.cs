using System;
using Vonage.Common.Client;

namespace Vonage.Server.Video.Archives.GetArchive;

/// <summary>
///     Represents a builder for ApplicationId.
/// </summary>
public interface IBuilderForApplicationId
{
    /// <summary>
    ///     Sets the ApplicationId.
    /// </summary>
    /// <param name="value">The ApplicationId.</param>
    /// <returns>The builder.</returns>
    IBuilderForArchiveId WithApplicationId(Guid value);
}

/// <summary>
///     Represents a builder for ArchiveId.
/// </summary>
public interface IBuilderForArchiveId
{
    /// <summary>
    ///     Sets the ArchiveId.
    /// </summary>
    /// <param name="value">The ArchiveId.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<GetArchiveRequest> WithArchiveId(Guid value);
}