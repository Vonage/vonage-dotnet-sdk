using System;

namespace Vonage.Common.Client.Builders;

/// <summary>
///     Indicates a request has a broadcast Id.
/// </summary>
public interface IHasBroadcastId
{
    /// <summary>
    ///     The broadcast Id.
    /// </summary>
    Guid BroadcastId { get; }
}