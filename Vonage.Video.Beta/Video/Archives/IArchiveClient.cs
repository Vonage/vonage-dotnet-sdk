using Vonage.Video.Beta.Video.Archives.ChangeLayout;
using Vonage.Video.Beta.Video.Archives.CreateArchive;
using Vonage.Video.Beta.Video.Archives.DeleteArchive;
using Vonage.Video.Beta.Video.Archives.GetArchive;
using Vonage.Video.Beta.Video.Archives.GetArchives;
using Vonage.Video.Beta.Video.Archives.StopArchive;

namespace Vonage.Video.Beta.Video.Archives;

/// <summary>
/// </summary>
public interface IArchiveClient :
    IGetArchivesUseCase,
    IGetArchiveUseCase,
    ICreateArchiveUseCase,
    IDeleteArchiveUseCase,
    IStopArchiveUseCase,
    IChangeLayoutUseCase
{
}