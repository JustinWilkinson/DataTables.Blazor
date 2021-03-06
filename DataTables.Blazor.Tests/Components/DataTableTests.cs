﻿using Bunit;
using DataTables.Blazor.Extensions;
using DataTables.Blazor.Interop;
using DataTables.Blazor.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DataTables.Blazor.Tests.Components
{
    public class DataTableTests
    {
        [Theory]
        [InlineData("id", "MyTable")]
        [InlineData("class", "table table-striped")]
        [InlineData("style", "width: 100%;")]
        [InlineData("data-test-id", "12345")]
        public void DataTable_GivenAttribute_RendersWithProvidedValue(string attributeName, string attributeValue)
        {
            // Arrange
            using var context = CreateContext();

            // Act
            var datatable = context.RenderComponent<DataTable>(parameters => parameters.Add(x => x.TableAttributes, new Dictionary<string, object>
            {
                [attributeName] = attributeValue
            }));

            // Assert
            var value = datatable.Find("table").GetAttribute(attributeName);
            Assert.Equal(attributeValue, value);
        }

        [Fact]
        public void DataTable_GivenSourceUrlAndNullAjaxOptions_CreatesAjaxOptions()
        {
            // Arrange
            using var context = CreateContext();
            var datatable = context.RenderComponent<DataTable>(parameters => parameters.Add(x => x.SourceUrl, "some url"));

            // Act
            var options = DataTableOptions.FromComponent(datatable.Instance);

            // Assert
            Assert.NotNull(options.Ajax);
            Assert.Equal("some url", options.Ajax.Url);
        }

        [Fact]
        public void DataTable_WithTwoColumns_RendersTwoColumnsWithCorrectTitles()
        {
            // Arrange
            using var context = CreateContext();

            // Act
            var datatable = context.RenderComponent<DataTable>(p => p
                .AddChildContent<Column>(c => c.Add(x => x.Title, "Title 1"))
                .AddChildContent<Column>(c => c.Add(x => x.Title, "Title 2")));

            // Assert
            var headers = datatable.Find("tr").Children;
            Assert.Collection(headers, x => Assert.Equal("Title 1", x.TextContent), y => Assert.Equal("Title 2", y.TextContent));
        }

        [Fact]
        public void DataTable_WithoutSourceUrl_WrapsInTableButRendersContentsAsIs()
        {
            // Arrange
            using var context = CreateContext();

            // Act
            var datatable = context.RenderComponent<DataTable>(p => p.AddChildContent("<thead><tr><th>Title 1</th><th>Title 2</th><tr></thead><tbody><tr><td>Value 1</td><td>Value 2</td></tr></tbody>"));

            // Assert
            var headers = datatable.Find("table > thead > tr").Children;
            var row = datatable.Find("table > tbody > tr").Children;
            Assert.Collection(headers, x => Assert.Equal("Title 1", x.TextContent), y => Assert.Equal("Title 2", y.TextContent));
            Assert.Collection(row, x => Assert.Equal("Value 1", x.TextContent), y => Assert.Equal("Value 2", y.TextContent));
        }

        [Fact]
        public void DataTable_WhenRendered_CallsInteropToInitialize()
        {
            // Arrange
            using var context = new TestContext();
            var interop = new Mock<IDataTablesInterop>();
            context.Services.AddTransient(sp => interop.Object);

            // Act
            var datatable = context.RenderComponent<DataTable>(parameters => parameters.Add(x => x.SourceUrl, "some url"));

            // Assert
            interop.Verify(x => x.InitialiseAsync(It.IsAny<ElementReference>(), It.IsAny<DataTableOptions>()), Times.Once);
        }

        [Fact]
        public void DataTable_WhenDisposed_CallsInteropToDestroy()
        {
            // Arrange
            using var context = new TestContext();
            var interop = new Mock<IDataTablesInterop>();
            context.Services.AddTransient(sp => interop.Object);

            // Act
            context.RenderComponent<DataTable>().Dispose();

            // Assert
            interop.Verify(x => x.InitialiseAsync(It.IsAny<ElementReference>(), It.IsAny<DataTableOptions>()), Times.Once);
        }

        private static TestContext CreateContext()
        {
            var context = new TestContext();
            context.Services.AddDataTables();
            context.JSInterop.Mode = JSRuntimeMode.Loose;
            return context;
        }
    }
}
