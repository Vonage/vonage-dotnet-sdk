using Vonage.Video.Beta.Video.Archives.CreateArchive;
using Vonage.Video.Beta.Video.Archives.DeleteArchive;
using Vonage.Video.Beta.Video.Archives.GetArchive;
using Vonage.Video.Beta.Video.Archives.GetArchives;

namespace Vonage.Video.Beta.Video.Archives;

/// <summary>
/// </summary>
public interface IArchiveClient : IGetArchivesUseCase, IGetArchiveUseCase, ICreateArchiveUseCase, IDeleteArchiveUseCase
{
}