using System.Collections.Generic;
using System.Linq;

namespace DataTables.Blazor.Options
{
    public class DataTableOptions
    {
        public Ajax Ajax { get; set; }

        //public string DOM { get; set; }

        public bool Order { get; set; }

        public bool Search { get; set; }

        public bool Paging { get; set; }

        public IEnumerable<ColumnOptions> Columns { get; set; }

        public static DataTableOptions FromComponent(DataTable table)
        {
            return new DataTableOptions
            {
                Ajax = new Ajax(table.Source),
                //DOM = table.DOM,
                Order = table.Order,
                Search = table.Search,
                Paging = table.Paging,
                Columns = table.Columns.Select(c => ColumnOptions.FromComponent(c))
            };
        }
    }

    public class Ajax
    {
        public Ajax(string source)
        {
            Url = source;
        }

        public string Url { get; set; }
    }
}