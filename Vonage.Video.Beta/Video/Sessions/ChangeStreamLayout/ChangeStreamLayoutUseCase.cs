using System;
using System.Threading.Tasks;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Video.Sessions.ChangeStreamLayout;

/// <inheritdoc />
public class ChangeStreamLayoutUseCase : IChangeStreamLayoutUseCase
{
    /// <inheritdoc />
    public Task<Result<Unit>> ChangeStreamLayoutAsync(ChangeStreamLayoutRequest request) =>
        throw new NotImplementedException();
}