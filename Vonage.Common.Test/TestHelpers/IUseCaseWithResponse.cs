namespace Vonage.Common.Test.TestHelpers;

public interface IUseCaseWithResponse : IUseCase
{
    Task ShouldReturnFailure_GivenApiResponseCannotBeParsed();
}