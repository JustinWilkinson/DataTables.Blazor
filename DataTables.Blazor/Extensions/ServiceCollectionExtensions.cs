using Microsoft.Extensions.DependencyInjection;

namespace DataTables.Blazor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataTables(this IServiceCollection services)
        {
            services.AddTransient<DataTablesInterop>();
        }
    }
}