using DataTables.Blazor.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
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
    }

    /// <inheritdoc/>
    internal class DataTablesInterop : IDataTablesInterop
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
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
#if DEBUG
            WriteIndented = true,
#endif
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
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
            => _runtime.InvokeVoidAsync("datatablesInterop.initialiseDataTable", tableReference, JsonSerializer.Serialize(options, _serializerOptions));

        /// <inheritdoc/>
        public ValueTask DestroyAsync(ElementReference tableReference)
            => _runtime.InvokeVoidAsync("datatablesInterop.destroyDataTable", tableReference);
    }
}