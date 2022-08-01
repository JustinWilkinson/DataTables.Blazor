using System;
using System.Collections.Generic;
using System.Text; 

namespace DataTables.Blazor.Events
{
    public class RowClickEventArgs : EventArgs
    {
        public int Index { get; set; }
        public string? Id { get; set; }
        public dynamic Data { get; set; } 
    }
}
