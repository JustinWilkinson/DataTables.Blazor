namespace DataTables.Blazor.Options
{
    /// <summary>
    /// Represents a DataTable column. See <a href="https://datatables.net/reference/option/columns">DataTables Reference</a> for more info.
    /// </summary>
    public class ColumnOptions
    { 
        public string CellType { get; set; }

        public string ClassName { get; set; }

        public string ContentPadding { get; set; }

        //public object CreatedCell { get; set; }

        public string Data { get; set; }

        public string DefaultContent { get; set; }

        public string Name { get; set; }

        public bool? Orderable { get; set; }

        public string OrderData { get; set; }

        public string OrderDataType { get; set; }

        //public object Render { get; set; }

        public bool? Searchable { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public bool? Visible { get; set; }

        public string Width { get; set; }

        public static ColumnOptions FromComponent(Column column)
        {
            return new ColumnOptions
            {
                CellType = column.CellType,
                ClassName = column.ClassName,
                ContentPadding = column.ContentPadding,
                // CreatedCell = column.CreatedCell,
                Data = column.Data,
                DefaultContent = column.DefaultContent,
                Name = column.Name,
                Orderable = column.Orderable,
                OrderData = column.OrderData,
                OrderDataType = column.OrderDataType,
                // Render = column.Render,
                Searchable = column.Searchable,
                Title = column.Title,
                Type = column.Type,
                Visible = column.Visible,
                Width = column.Width,
            };
        }
    }
}