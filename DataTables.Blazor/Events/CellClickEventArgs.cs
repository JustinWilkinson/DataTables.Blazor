using System;
using System.Collections.Generic;
using System.Text; 

namespace DataTables.Blazor.Events
{
    public class CellClickEventArgs : EventArgs
    {
        /// <summary>
        /// Index of the column starts with 0. Hidden columns will also be taken into account.
        /// Additional info: https://datatables.net/reference/api/column().index()
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// Index of the row starts with 0. 
        /// Additional info: https://datatables.net/reference/api/row().index()
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// The id of the row this cell is on. The value will be null if no <see href="https://datatables.net/reference/option/rowId">rowId</see> option is specified.
        /// </summary>
        public string RowId { get; set; }

        /// <summary>
        /// Value of cell.
        /// </summary>
        public string Data { get; set; } 
    }
}
