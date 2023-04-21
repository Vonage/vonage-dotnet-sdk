using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.ProactiveConnect.Lists.GetLists;

namespace Vonage.ProactiveConnect;

/// <summary>
///     Exposes Proactive Connect features.
/// </summary>
public interface IProactiveConnectClient
{
    /// <summary>
    ///     Retrieves all lists.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<GetListsResponse>> GetListsAsync(Result<GetListsRequest> request);
}