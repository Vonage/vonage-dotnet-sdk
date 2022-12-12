namespace Vonage.Voice;

public interface ITokenGenerator
{
    string GenerateToken(string applicationId, string privateKey);
}