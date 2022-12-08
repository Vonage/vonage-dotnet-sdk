namespace Vonage.Video.Beta.Common;

public struct ResultFailure
{
    public string Error { get; }

    public ResultFailure(string error) => this.Error = error;
}