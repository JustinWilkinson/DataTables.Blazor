using System;
using System.Collections.Generic;
using System.Text; 

namespace DataTables.Blazor.Events
{
    public class CellClickEventArgs : EventArgs
    {
        public int ColumnId { get; set; }
        public string? RowIndex { get; set; }
        public string? RowId { get; set; }
        public dynamic Data { get; set; } 
    }
}
