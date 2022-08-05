using System;
using System.Collections.Generic;
using System.Text; 

namespace DataTables.Blazor.Events
{
    public class RowClickEventArgs : EventArgs
    {
        /// <summary>
        /// Index of the row starts with 0. 
        /// Additional info: https://datatables.net/reference/api/row().index()
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The id of the row. The value will be null if no <see href="https://datatables.net/reference/option/rowId">rowId</see> option is specified.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A dynamic object containing data of type property value, id column name and cell value.
        /// </summary>
        public dynamic Data { get; set; } 
    }
}
