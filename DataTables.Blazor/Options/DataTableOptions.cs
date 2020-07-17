using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace DataTables.Blazor.Options
{
    /// <summary>
    /// See <a href="https://datatables.net/reference/option/">DataTables Reference</a> for more info.
    /// </summary>
    public class DataTableOptions
    {
        #region Features
        public bool? AutoWidth { get; set; }

        public bool? DeferRender { get; set; }

        public bool? Info { get; set; }

        public bool? LengthChange { get; set; }

        public bool? Ordering { get; set; }

        public bool? Paging { get; set; }

        public bool? Processing { get; set; }

        public bool? ScrollX { get; set; }

        public string ScrollY { get; set; }

        public bool? Searching { get; set; }

        public bool? ServerSide { get; set; }

        public bool? StateSave { get; set; }
        #endregion

        #region Data
        public AjaxOptions Ajax { get; set; }

        public object Data { get; set; }
        #endregion

        #region Columns
        public IEnumerable<ColumnOptions> Columns { get; private set; }

        public IEnumerable<ColumnDefOptions> ColumnDefs { get; set; }
        #endregion

        #region Options
        public object DeferLoading { get; set; } // Integer or integer array

        public bool? Destroy { get; set; }

        public int? DisplayStart { get; set; }

        [JsonPropertyName("dom")]
        public string DOM { get; set; }

        public IEnumerable<int> LengthMenu { get; set; }

        public IEnumerable<object[]> Order { get; set; }

        public bool? OrderCellsTop { get; set; }

        public bool? OrderClasses { get; set; }

        public IEnumerable<object[]> OrderFixed { get; set; }

        public bool? OrderMulti { get; set; }

        public int? PageLength { get; set; }

        public string PagingType { get; set; }

        public object Renderer { get; set; }

        public bool? Retrieve { get; set; }

        public string RowId { get; set; }

        public bool? ScrollCollapse { get; set; }

        public SearchOptions Search { get; set; }

        public IEnumerable<SearchOptions> SearchCols { get; set; }

        public int? SearchDelay { get; set; }

        public int? StateDuration { get; set; }

        public string StripeClasses { get; set; }

        public int? TabIndex { get; set; }
        #endregion

        public static DataTableOptions FromComponent(DataTable table)
        {
            var options = table.Options ?? new DataTableOptions();
            options.Columns = table.Columns.Select(c => ColumnOptions.FromComponent(c));
            if (table.SourceUrl != null && options.Ajax == null)
            {
                options.Ajax = new AjaxOptions(table.SourceUrl);
            }
            return options;
        }
    }
}