using DataTables.Blazor.Extensions;
using DataTables.Blazor.Interop;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DataTables.Blazor.Tests.Extensions;

public class ServiceCollectionExtensionTests
{
    [Fact]
    public void AddDataTables_WhenCalledWithJSRuntime_ShouldAddRequiredServices()
    {
        // Arrange
        var provider = new ServiceCollection().AddTransient<IJSRuntime, StubJSRuntime>().AddDataTables().BuildServiceProvider();

        // Act
        var service = provider.GetService<IDataTablesInterop>();

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void AddDataTables_WhenCalledWithoutJSRuntime_ShouldThrowException()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act/Assert
        Assert.Throws<InvalidOperationException>(() => services.AddDataTables());
    }
}

internal class StubJSRuntime : IJSRuntime
{
    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object[] args) => default;

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object[] args) => default;
}