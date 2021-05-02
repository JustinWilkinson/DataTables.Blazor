using System;
using System.Collections.Generic;

namespace DataTables.Blazor.Options
{
    /// <summary>
    /// Represents a columnDef for a DataTable. See <a href="https://datatables.net/reference/option/columnDefs">DataTables Reference</a> for more info.
    /// </summary>
    public class ColumnDefOptions : ColumnOptions
    {
        private object _targets;

        /// <summary>
        /// Can be an integer or integer array, or string.
        /// </summary>
        public object Targets 
        { 
            get => _targets;
            set
            {
                if (value is null || value is string || value is int || value is IEnumerable<int>)
                {
                    _targets = value;
                }
                else
                {
                    throw new InvalidOperationException("Value must be a string, integer or an integer array.");
                }
            }
        }
    }
}
