namespace DataTables.Blazor.Options
{
    public class ColumnOptions
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public string DefaultContent { get; set; }

        public string ClassName { get; set; }

        public bool Searchable { get; set; }

        public bool Orderable { get; set; }

        public string OrderData { get; set; }

        public bool Visible { get; set; } = true;

        public string Width { get; set; }

        public static ColumnOptions FromComponent(Column column)
        {
            return new ColumnOptions
            {
                Title = column.Title,
                Name = column.Name,
                Data = column.Data,
                DefaultContent = column.DefaultContent,
                ClassName = column.ClassName,
                Searchable = column.Searchable,
                Orderable = column.Orderable,
                OrderData = column.OrderData,
                Visible = column.Visible,
                Width = column.Width,
            };
        }
    }
}