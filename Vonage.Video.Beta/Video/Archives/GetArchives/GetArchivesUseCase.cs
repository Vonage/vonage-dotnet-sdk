using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Video.Archives.GetArchives;

/// <inheritdoc />
public class GetArchivesUseCase : IGetArchivesUseCase
{
    /// <inheritdoc />
    public Task<Result<GetArchivesResponse>> GetStreamsAsync(GetArchivesRequest request) =>
        throw new NotImplementedException();
}