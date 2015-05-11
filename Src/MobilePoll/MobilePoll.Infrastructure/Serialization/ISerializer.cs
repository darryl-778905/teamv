using System;

namespace MobilePoll.Infrastructure.Serialization
{
    public interface ISerializer
    {
        T Deserialize<T>(string value);
        object Deserialize(string value, Type type);
        object Deserialize<T>(string value, Type type);
        string Serialize(object value);
        byte[] ToByteArray(object value);
        T FromByteArray<T>(byte[] value);
    }
}
