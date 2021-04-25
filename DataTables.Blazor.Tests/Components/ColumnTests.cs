using Bunit;
using Xunit;

namespace DataTables.Blazor.Tests.Components
{
    public class ColumnTests
    {
        [Fact]
        public void Column_WhenRendered_ShouldHaveTitleAsContent()
        {
            // Arrange
            using var context = new TestContext();

            // Act
            var column = context.RenderComponent<Column>(parameters => parameters.Add(x => x.Table, new DataTable()).Add(x => x.Title, "Hello World"));

            // Assert
            var content = column.Find("th").TextContent;
            Assert.Equal("Hello World", content);
        }
    }
}
