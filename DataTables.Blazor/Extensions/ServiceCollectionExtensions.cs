using DataTables.Blazor.Interop;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;

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
        public static IServiceCollection AddDataTables(this IServiceCollection services)
        {
            var service = services.BuildServiceProvider().GetService<IJSRuntime>();

            if (service is null)
            {
                throw new InvalidOperationException($"DataTables.Blazor requires an implementation of {typeof(IJSRuntime).FullName} to be registered in order to run.");
            }

            services.AddTransient<IDataTablesInterop, DataTablesInterop>();
            
            services.AddTransient<IDomEventListener, DomEventListener>();

            return services;
        }
    }
}