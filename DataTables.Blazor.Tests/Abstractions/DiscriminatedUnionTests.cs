using DataTables.Blazor.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataTables.Blazor.Tests.Abstractions
{
    public class DiscriminatedUnionTests
    {
        [Fact]
        public void DiscriminatedUnion_TwoTypes_ThrowsIfTypeNotAllowed()
        {
            // Arrange
            var union = new DiscriminatedUnion<int, string>();

            // Act/Assert
            var ex = Assert.Throws<InvalidOperationException>(() => union.Value = 'C');
            Assert.Equal("Type 'Char' is not a valid type for this value. Valid values are: 'Int32', 'String'.", ex.Message);
        }

        [Fact]
        public void DiscriminatedUnion_ThreeTypes_ThrowsIfTypeNotAllowed()
        {
            // Arrange
            var union = new DiscriminatedUnion<int, string, long>();

            // Act/Assert
            var ex = Assert.Throws<InvalidOperationException>(() => union.Value = 'C');
            Assert.Equal("Type 'Char' is not a valid type for this value. Valid values are: 'Int32', 'String', 'Int64'.", ex.Message);
        }

        [Fact]
        public void DiscriminatedUnion_FourTypes_ThrowsIfTypeNotAllowed()
        {
            // Arrange
            var union = new DiscriminatedUnion<int, string, long, decimal>();

            // Act/Assert
            var ex = Assert.Throws<InvalidOperationException>(() => union.Value = 'C');
            Assert.Equal("Type 'Char' is not a valid type for this value. Valid values are: 'Int32', 'String', 'Int64', 'Decimal'.", ex.Message);
        }

        [Fact]
        public void DiscriminatedUnion_TwoTypes_SetsValueIfAllowed()
        {
            // Arrange
            var union = new DiscriminatedUnion<int, string>();

            // Act/Assert
            union.Value = 1;
            Assert.IsType<int>(union.Value);
            Assert.Equal(1, union.Value);

            union.Value = "Hello, World!";
            Assert.IsType<string>(union.Value);
            Assert.Equal("Hello, World!", union.Value);
        }

        [Fact]
        public void DiscriminatedUnion_ThreeTypes_SetsValueIfAllowed()
        {
            // Arrange
            var union = new DiscriminatedUnion<int, string, long>();

            // Act/Assert
            union.Value = 1;
            Assert.IsType<int>(union.Value);
            Assert.Equal(1, union.Value);

            union.Value = "Hello, World!";
            Assert.IsType<string>(union.Value);
            Assert.Equal("Hello, World!", union.Value);

            union.Value = long.MaxValue;
            Assert.IsType<long>(union.Value);
            Assert.Equal(long.MaxValue, union.Value);
        }

        [Fact]
        public void DiscriminatedUnion_FourTypes_SetsValueIfAllowed()
        {
            // Arrange
            var union = new DiscriminatedUnion<int, string, long, decimal>();

            // Act/Assert
            union.Value = 1;
            Assert.IsType<int>(union.Value);
            Assert.Equal(1, union.Value);

            union.Value = "Hello, World!";
            Assert.IsType<string>(union.Value);
            Assert.Equal("Hello, World!", union.Value);

            union.Value = long.MaxValue;
            Assert.IsType<long>(union.Value);
            Assert.Equal(long.MaxValue, union.Value);

            union.Value = 1.2M;
            Assert.IsType<decimal>(union.Value);
            Assert.Equal(1.2M, union.Value);
        }

        [Fact]
        public void ImplicitConversion_TwoTypes_SetsValueWhenSet()
        {
            // Arrange
            DiscriminatedUnion<int, string> union;

            // Act/Assert
            union = 1;
            Assert.IsType<int>(union.Value);
            Assert.Equal(1, union.Value);

            union = "Hello, World!";
            Assert.IsType<string>(union.Value);
            Assert.Equal("Hello, World!", union.Value);
        }


        [Fact]
        public void DiscriminatedUnion_ThreeTypes_SetsValueWhenSet()
        {
            // Arrange
            DiscriminatedUnion<int, string, long> union;

            // Act/Assert
            union = 1;
            Assert.IsType<int>(union.Value);
            Assert.Equal(1, union.Value);

            union = "Hello, World!";
            Assert.IsType<string>(union.Value);
            Assert.Equal("Hello, World!", union.Value);

            union = long.MaxValue;
            Assert.IsType<long>(union.Value);
            Assert.Equal(long.MaxValue, union.Value);
        }

        [Fact]
        public void DiscriminatedUnion_FourTypes_SetsValueWhenSet()
        {
            // Arrange
            DiscriminatedUnion<int, string, long, decimal> union;

            // Act/Assert
            union = 1;
            Assert.IsType<int>(union.Value);
            Assert.Equal(1, union.Value);

            union = "Hello, World!";
            Assert.IsType<string>(union.Value);
            Assert.Equal("Hello, World!", union.Value);

            union = long.MaxValue;
            Assert.IsType<long>(union.Value);
            Assert.Equal(long.MaxValue, union.Value);

            union = 1.2M;
            Assert.IsType<decimal>(union.Value);
            Assert.Equal(1.2M, union.Value);
        }
    }
}
