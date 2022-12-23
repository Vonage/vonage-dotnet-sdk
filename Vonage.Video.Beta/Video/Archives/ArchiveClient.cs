using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video.Archives.GetArchives;

namespace Vonage.Video.Beta.Video.Archives;

/// <inheritdoc />
public class ArchiveClient : IArchiveClient
{
    /// <inheritdoc />
    public Task<Result<GetArchivesResponse>> GetStreamsAsync(GetArchivesRequest request) =>
        throw new NotImplementedException();
}