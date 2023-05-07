namespace DataTables.Blazor.Options;

/// <summary>
/// Represents the search options for a DataTable. See <a href="https://datatables.net/reference/option/search">DataTables Reference</a> for more info.
/// </summary>
public class SearchOptions
{
    public bool? CaseInsensitive { get; set; }

    public bool? Regex { get; set; }

    public string Search { get; set; }

    public bool? Smart { get; set; }
}