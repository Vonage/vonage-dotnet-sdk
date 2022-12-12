namespace Vonage.Video.Beta.Common;

public interface IJsonSerializer
{
    string SerializeObject<T>(T value);

    Result<T> DeserializeObject<T>(string serializedValue);
}