using DataTables.Blazor.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace DataTables.Blazor
{
    internal class DataTablesInterop
    {
        private readonly IJSRuntime _runtime;

        public DataTablesInterop(IJSRuntime runtime)
        {
            _runtime = runtime;
        }

        public async ValueTask InitialiseAsync(string id, DataTableOptions options) => await _runtime.InvokeVoidAsync("datatablesInterop.initialiseDataTable", id, options);
    }

    public static class ServiceCollectionExtensions
    {
        public static void AddDataTables(this IServiceCollection services)
        {
            services.AddTransient<DataTablesInterop>();
        }
    }
}