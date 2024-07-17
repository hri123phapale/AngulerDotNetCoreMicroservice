using Newtonsoft.Json;
using System;

namespace Blogposts.Common.Utilities.Converters
{
    public class TypeConverter<TImplementation, TAbstraction> : JsonConverter where TImplementation : TAbstraction
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(TAbstraction);

        public override object ReadJson(JsonReader reader, Type type, object value, JsonSerializer serializer)
        {
            return serializer.Deserialize<TImplementation>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}