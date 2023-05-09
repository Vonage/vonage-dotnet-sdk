using FsCheck;

namespace Vonage.Common.Test.TestHelpers;

public interface IUseCase
{
    Property ShouldReturnFailure_GivenApiErrorCannotBeParsed();

    Property ShouldReturnFailure_GivenApiResponseIsError();

    Task ShouldReturnFailure_GivenRequestIsFailure();

    Task ShouldReturnFailure_GivenTokenGenerationFailed();

    Task ShouldReturnSuccess_GivenApiResponseIsSuccess();
}