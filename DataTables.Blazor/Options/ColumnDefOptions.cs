using DataTables.Blazor.Abstractions;
using System.Collections.Generic;

namespace DataTables.Blazor.Options;

/// <summary>
/// Represents a columnDef for a DataTable. See <a href="https://datatables.net/reference/option/columnDefs">DataTables Reference</a> for more info.
/// </summary>
public class ColumnDefOptions : ColumnOptions
{
    /// <summary>
    /// Can be an integer or integer array, or string.
    /// </summary>
    public DiscriminatedUnion<string, int, IEnumerable<int>> Targets { get; set; }
}