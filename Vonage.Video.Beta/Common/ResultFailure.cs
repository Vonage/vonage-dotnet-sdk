namespace Vonage.Video.Beta.Common;

public struct ResultFailure
{
    public string Error { get; }

    private ResultFailure(string error) => this.Error = error;

    public static ResultFailure FromErrorMessage(string error) => new(error);
}