namespace DataTables.Blazor.Options;

/// <summary>
/// Represents the language options for a DataTable. See <a href="https://datatables.net/reference/option/language">DataTables Reference</a> for more info.
/// </summary>
public class LanguageOptions
{
    public AriaOptions Aria { get; set; }

    public string Decimal { get; set; }

    public string EmptyTable { get; set; }

    public string Info { get; set; }

    public string InfoEmpty { get; set; }

    public string InfoFiltered { get; set; }

    public string InfoPostFix { get; set; }

    public string LengthMenu { get; set; }

    public string LoadingRecords { get; set; }

    public PaginateOptions Paginate { get; set; }

    public object Buttons { get; set; }

    public string Processing { get; set; }

    public string Search { get; set; }

    public string SearchPlaceholder { get; set; }

    public string Thousands { get; set; }

    public string Url { get; set; }

    public string ZeroRecords { get; set; }
}