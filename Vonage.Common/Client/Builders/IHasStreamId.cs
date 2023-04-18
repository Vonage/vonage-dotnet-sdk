using System;

namespace Vonage.Common.Client.Builders;

/// <summary>
///     Indicates the request has a StreamId.
/// </summary>
public interface IHasStreamId
{
    /// <summary>
    ///     The stream Id.
    /// </summary>
    public Guid StreamId { get; }
}