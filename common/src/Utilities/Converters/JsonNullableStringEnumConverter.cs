using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blogposts.Common.Utilities.Converters
{
    /// <summary>
    /// A converter that support nullable enums
    /// This is supported in .NET 5 OOB
    /// </summary>
    public class JsonNullableStringEnumConverter : JsonConverterFactory
    {
        #region Private Members

        private readonly JsonStringEnumConverter _stringEnumConverter;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor with params
        /// </summary>
        /// <param name="namingPolicy"></param>
        /// <param name="allowIntegerValues"></param>
        public JsonNullableStringEnumConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
        {
            _stringEnumConverter = new JsonStringEnumConverter(namingPolicy, allowIntegerValues);
        }

        #endregion

        #region Public Methods

        public override bool CanConvert(Type typeToConvert)
            => Nullable.GetUnderlyingType(typeToConvert)?.IsEnum == true;

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var type = Nullable.GetUnderlyingType(typeToConvert)!;
            return (JsonConverter)Activator.CreateInstance(typeof(ValueConverter<>).MakeGenericType(type),
                _stringEnumConverter.CreateConverter(type, options));
        }

        #endregion

        private class ValueConverter<T> : JsonConverter<T?>
            where T : struct, Enum
        {
            private readonly JsonConverter<T> _converter;

            public ValueConverter(JsonConverter<T> converter)
            {
                _converter = converter;
            }

            public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    reader.Read();
                    return null;
                }
                return _converter.Read(ref reader, typeof(T), options);
            }

            public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
            {
                if (value == null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    _converter.Write(writer, value.Value, options);
                }
            }
        }
    }
}