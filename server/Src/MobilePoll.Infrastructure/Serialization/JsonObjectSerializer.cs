using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization.Formatters;
using System.Text;
using MobilePoll.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MobilePoll.Infrastructure.Serialization
{
    [DebuggerNonUserCode, DebuggerStepThrough]
    public class JsonObjectSerializer : ISerializer
    {
        static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            TypeNameHandling = TypeNameHandling.All,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Populate,
            Converters = { new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.RoundtripKind }}
        };

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, SerializerSettings);
        }

        public object Deserialize<T>(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type, SerializerSettings);
        }

        public object Deserialize(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type, SerializerSettings);
        }

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public byte[] ToByteArray(object value)
        {
            var serialized = Serialize(value);
            return Encoding.UTF8.GetBytes(serialized);
        }

        public T FromByteArray<T>(byte[] value)
        {
            var serialized = Encoding.UTF8.GetString(value);
            return Deserialize<T>(serialized);
        }
    }
}