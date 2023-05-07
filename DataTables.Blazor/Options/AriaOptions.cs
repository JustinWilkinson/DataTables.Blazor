namespace DataTables.Blazor.Options;

/// <summary>
/// Represents the aria options for a DataTable. See <a href="https://datatables.net/reference/option/language.aria">DataTables Reference</a> for more info.
/// </summary>
public class AriaOptions
{
    public PaginateOptions Paginate { get; set; }

    public string SortAscending { get; set; }

    public string SortDescending { get; set; }
}