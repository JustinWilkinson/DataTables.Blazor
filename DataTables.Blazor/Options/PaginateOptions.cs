namespace DataTables.Blazor.Options
{
    /// <summary>
    /// Represents the pagination language options for a DataTable. See <a href="https://datatables.net/reference/option/language.paginate">DataTables Reference</a> for more info.
    /// </summary>
    public class PaginateOptions
    {
        public string First { get; set; }

        public string Last { get; set; }

        public string Next { get; set; }

        public string Previous { get; set; }
    }
}