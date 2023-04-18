using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Common.Client.Builders;

public static class BuilderExtensions
{
    public static Result<T> VerifyApplicationId<T>(T request) where T : IHasApplicationId =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    public static Result<T> VerifyArchiveId<T>(T request) where T : IHasArchiveId =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(request.ArchiveId));

    public static Result<T> VerifyStreamId<T>(T request) where T : IHasStreamId =>
        InputValidation.VerifyNotEmpty(request, request.StreamId, nameof(request.StreamId));
    
    

    public static Result<T> VerifySessionId<T>(T request) where T : IHasSessionId =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}