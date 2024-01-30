using System;

namespace Vonage.Common.Client.Builders;

/// <summary>
///     Indicates a request has an ApplicationId.
/// </summary>
public interface IHasApplicationId
{
    /// <summary>
    ///     The Vonage Application UUID.
    /// </summary>
    public Guid ApplicationId { get; }
}