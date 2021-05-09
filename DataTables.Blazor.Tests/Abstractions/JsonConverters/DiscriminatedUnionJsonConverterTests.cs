using DataTables.Blazor.Abstractions;
using DataTables.Blazor.Abstractions.JsonConverters;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace DataTables.Blazor.Tests.Abstractions.JsonConverters
{
    public class DiscriminatedUnionJsonConverterTests
    {
        private static readonly JsonSerializerOptions _serializerOptions = new()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new DiscriminatedUnionJsonConverter() }
        };

        [Fact]
        public void Write_DiscriminatedUnionAsInteger_WritesValueAsJson()
        {
            // Arrange
            DiscriminatedUnion<int, IEnumerable<int>> union = 1;

            // Act
            var result = JsonSerializer.Serialize(union, _serializerOptions);

            // Assert
            Assert.Equal("1", result);
        }


        [Fact]
        public void Write_DiscriminatedUnionAsBool_WritesValueAsJson()
        {
            // Arrange
            DiscriminatedUnion<int, bool> union = true;

            // Act
            var result = JsonSerializer.Serialize(union, _serializerOptions);

            // Assert
            Assert.Equal("true", result);
        }

        [Fact]
        public void Write_DiscriminatedUnionAsString_WritesValueAsJson()
        {
            // Arrange
            DiscriminatedUnion<int, string> union = "Hello, World!";

            // Act
            var result = JsonSerializer.Serialize(union, _serializerOptions);

            // Assert
            Assert.Equal(@"""Hello, World!""", result);
        }

        [Fact]
        public void Write_DiscriminatedUnionAsAraryOfIntegers_WritesValueAsJson()
        {
            // Arrange
            DiscriminatedUnion<int, IEnumerable<int>> union = new[] { 1, 2, 3 };

            // Act
            var result = JsonSerializer.Serialize(union, _serializerOptions);

            // Assert
            Assert.Equal("[1,2,3]", result);
        }


        [Fact]
        public void Write_DiscriminatedUnionAsUserDefinedType_WritesValueAsJson()
        {
            // Arrange
            DiscriminatedUnion<int, TestClass> union = new TestClass
            {
                Property1 = "Hello, World!",
                Property2 = 1,
                Property3 = 1.2M,
                Property4 = true,
                Property5 = new[] { 1, 2, 3 }
            };

            // Act
            var result = JsonSerializer.Serialize(union, _serializerOptions);

            // Assert
            Assert.Equal(@"{""property1"":""Hello, World!"",""property2"":1,""property3"":1.2,""property4"":true,""property5"":[1,2,3]}", result);
        }

        [Fact]
        public void Write_DiscriminatedUnionAsObjectProperty_WritesValueAsJson()
        {
            // Arrange
            var anonymous = new
            {
                Union = new DiscriminatedUnion<int, IEnumerable<int>> { Value = new[] { 1, 2, 3 } }
            };

            // Act
            var result = JsonSerializer.Serialize(anonymous, _serializerOptions);

            // Assert
            Assert.Equal(@"{""union"":[1,2,3]}", result);
        }

        private class TestClass
        {
            public string Property1 { get; set; }

            public int Property2 { get; set; }

            public decimal Property3 { get; set; }

            public bool Property4 { get; set; }

            public int[] Property5 { get; set; }
        }
    }
}
