using Microsoft.Extensions.DependencyInjection;

namespace DataTables.Blazor.Extensions
{
    /// <summary>
    /// Contains extension methods on the service collection class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the services necessary for DataTables.Blazor to the service collection.
        /// </summary>
        /// <param name="services">The service collection to add to.</param>
        /// <returns>The modified service collection for method chaining.</returns>
        public static IServiceCollection AddDataTables(this IServiceCollection services) => services.AddTransient<DataTablesInterop>();
    }
}