namespace DataTables.Blazor.Options
{
    /// <summary>
    /// Represents a columnDef for a DataTable. See <a href="https://datatables.net/reference/option/columnDefs">DataTables Reference</a> for more info.
    /// </summary>
    public class ColumnDefOptions : ColumnOptions
    {
        public object Targets { get; set; } // Integer or integer array, or string.
    }
}
