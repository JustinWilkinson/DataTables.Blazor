using DataTables.Blazor.Abstractions;
using DataTables.Blazor.Abstractions.JsonConverters;
using DataTables.Blazor.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Text.Json;
#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#endif
using System.Threading.Tasks;

namespace DataTables.Blazor.Interop
{
    /// <summary>
    /// Controls the communication between .NET and JS for DataTables.Blazor.
    /// </summary>
    public interface IDataTablesInterop
    {
        /// <summary>
        /// Initialises a DataTable with the provided options on the provided element.
        /// </summary>
        /// <param name="tableReference">Reference of element to add DataTable to.</param>
        /// <param name="options">Options for DataTable.</param>
        ValueTask InitialiseAsync(ElementReference tableReference, DataTableOptions options);

        /// <summary>
        /// Destroys the DataTable for the provided element.
        /// </summary>
        /// <param name="tableReference">Reference of element to add DataTable to.</param>
        ValueTask DestroyAsync(ElementReference tableReference);

        /// <summary>
        /// Reload the DataTable for the provided element from the source url.
        /// </summary>
        /// <param name="tableReference">Reference to the DataTable.</param>
        ValueTask AjaxReloadAsync(ElementReference tableReference);

        /// <summary>
        /// Reload the DataTable for the provided element from an <see cref="IDataset"/> source.
        /// </summary>
        /// <param name="tableReference">Reference to the DataTable.</param>
        /// <param name="dataset">The dataset to use.</param>
        ValueTask ReloadAsync(ElementReference tableReference, IDataset dataset);

        /// <summary>
        ///  
        /// </summary>
        /// <param name="tableReference">Reference to the DataTable.</param>
        ValueTask AddEventListenerAsync(ElementReference tableReference, string eventName, Object dotNetCallback);
    }

    /// <inheritdoc/>
    internal sealed class DataTablesInterop : IDataTablesInterop
    {
        /// <summary>
        /// Runtime used by the interop.
        /// </summary>
        private readonly IJSRuntime _runtime;

        /// <summary>
        /// We need to strip null values from the options, as these are treated differently to "undefined" by DataTables.
        /// To do so, we serialize the options ourselves using the specified options.
        /// This is then deserialized in dataTablesInterop.js.
        /// </summary>
        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
#if NET6_0_OR_GREATER
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
#else
            IgnoreNullValues = true,
#endif
            PropertyNameCaseInsensitive = true,
#if DEBUG
            WriteIndented = true,
#endif
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new DiscriminatedUnionJsonConverter() }
        };

        /// <summary>
        /// Should be instantiated from service collection.
        /// </summary>
        /// <param name="runtime">The IJSRuntime to use.</param>
        public DataTablesInterop(IJSRuntime runtime)
        {
            _runtime = runtime;
        }

        /// <inheritdoc/>
        public ValueTask InitialiseAsync(ElementReference tableReference, DataTableOptions options) 
            => _runtime.InvokeVoidAsync("datatablesInterop.initialiseDataTable", tableReference, JsonSerializer.Serialize(options, SerializerOptions));

        /// <inheritdoc/>
        public ValueTask DestroyAsync(ElementReference tableReference)
            => _runtime.InvokeVoidAsync("datatablesInterop.destroyDataTable", tableReference);

        /// <inheritdoc/>
        public ValueTask AjaxReloadAsync(ElementReference tableReference)
            => _runtime.InvokeVoidAsync("datatablesInterop.ajaxReloadDataTable", tableReference);

        /// <inheritdoc/>
        public ValueTask ReloadAsync(ElementReference tableReference, IDataset dataset)
            => _runtime.InvokeVoidAsync("datatablesInterop.reloadDataTable", tableReference, JsonSerializer.Serialize(dataset, SerializerOptions));

        /// <inheritdoc/>
        public ValueTask AddEventListenerAsync(ElementReference tableReference, string eventName, Object dotNetCallback)
            => _runtime.InvokeVoidAsync("datatablesInterop.addEventListener", tableReference, eventName, dotNetCallback);
    }
}