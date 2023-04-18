using System;

namespace Vonage.Common.Client.Builders;

/// <summary>
///     Indicates the request has an ArchiveId.
/// </summary>
public interface IHasArchiveId
{
    /// <summary>
    ///     The archive Id.
    /// </summary>
    public Guid ArchiveId { get; }
}