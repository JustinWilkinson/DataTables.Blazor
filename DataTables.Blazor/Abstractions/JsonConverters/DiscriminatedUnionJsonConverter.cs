using DataTables.Blazor.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

[assembly: InternalsVisibleTo("DataTables.Blazor.Tests")]
namespace DataTables.Blazor.Abstractions.JsonConverters
{
    internal class DiscriminatedUnionJsonConverter : JsonConverter<DiscriminatedUnion>
    {
        private const int MaxDepth = 50;

        private static readonly Dictionary<Type, Dictionary<string, Func<object, object>>> _getters = new Dictionary<Type, Dictionary<string, Func<object, object>>>();

        public override bool CanConvert(Type typeToConvert)
            => typeof(DiscriminatedUnion).IsAssignableFrom(typeToConvert);

        public override DiscriminatedUnion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new NotImplementedException();

        public override void Write(Utf8JsonWriter writer, DiscriminatedUnion value, JsonSerializerOptions options)
            => Write(writer, value.Value);

        private static void Write(Utf8JsonWriter writer, object value, int depth = 0)
        {
            if (++depth >= MaxDepth)
            {
                throw new InvalidOperationException("Maximum recursive depth exceeded!");
            }

            if (value is null)
            {
                writer.WriteNullValue();
            }
            else if (value is string str)
            {
                writer.WriteStringValue(str);
            }
            else if (value is bool b)
            {
                writer.WriteBooleanValue(b);
            }
            else if (value is byte bt)
            {
                writer.WriteNumberValue(bt);
            }
            else if (value is sbyte sbt)
            {
                writer.WriteNumberValue(sbt);
            }
            else if (value is short sh)
            {
                writer.WriteNumberValue(sh);
            }
            else if (value is ushort ush)
            {
                writer.WriteNumberValue(ush);
            }
            else if (value is int integer)
            {
                writer.WriteNumberValue(integer);
            }
            else if (value is uint unsignedInteger)
            {
                writer.WriteNumberValue(unsignedInteger);
            }
            else if (value is long l)
            {
                writer.WriteNumberValue(l);
            }
            else if (value is ulong ul)
            {
                writer.WriteNumberValue(ul);
            }
            else if (value is float f)
            {
                writer.WriteNumberValue(f);
            }
            else if (value is double db)
            {
                writer.WriteNumberValue(db);
            }
            else if (value is decimal dc)
            {
                writer.WriteNumberValue(dc);
            }
            else if (value is IEnumerable ie)
            {
                writer.WriteStartArray();

                foreach (var item in ie)
                {
                    Write(writer, item, depth);
                }

                writer.WriteEndArray();
            }
            else
            {
                writer.WriteStartObject();

                var type = value.GetType();
                if (!_getters.TryGetValue(type, out var gettersByName))
                {
                    gettersByName = type.GetProperties().ToDictionary(x => x.Name, x => x.GetGetter(x.DeclaringType));
                    _getters.Add(type, gettersByName);
                }

                foreach (var getter in gettersByName)
                {
                    writer.WritePropertyName(getter.Key.ToCamelCase());
                    Write(writer, getter.Value(value));
                }

                writer.WriteEndObject();
            }
        }
    }
}
