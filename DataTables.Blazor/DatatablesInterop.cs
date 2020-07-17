using DataTables.Blazor.Options;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataTables.Blazor
{
    internal class DataTablesInterop
    {
        private readonly IJSRuntime _runtime;
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
#if DEBUG
            WriteIndented = true,
#endif
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public DataTablesInterop(IJSRuntime runtime)
        {
            _runtime = runtime;
        }

        public async ValueTask InitialiseAsync(string id, DataTableOptions options)
        {
            // We need to strip null values from the options, as these are treated differently to "undefined" by DataTables.
            // To do so, we serialize the options ourselves using the specified options. This is then deserialized in dataTablesInterop.js.
            await _runtime.InvokeVoidAsync("datatablesInterop.initialiseDataTable", id, JsonSerializer.Serialize(options, _serializerOptions));
        }
    }
}