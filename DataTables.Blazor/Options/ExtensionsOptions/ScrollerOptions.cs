using System;
using System.Collections.Generic;
using System.Text;

namespace DataTables.Blazor.Options.ExtensionsOptions
{
    /// <summary>
    /// Represents the scroll extension options for a DataTable. 
    /// See <a href="https://datatables.net/extensions/scroller/">DataTables Reference</a> for more info.
    /// </summary>
    public class ScrollerOptions
    {
        /// <summary>
        /// Set the point at which new data will be loaded and drawn.
        /// </summary>
        public string BoundaryScale { get; set; }

        /// <summary>
        /// The amount of data that Scroller should pre-buffer to ensure smooth scrolling.
        /// </summary>
        public string DisplayBuffer { get; set; }

        /// <summary> 
        /// Display a loading message while Scroller is loading additional data.
        /// </summary>
        public bool LoadingIndicator { get; set; }

        /// <summary>
        /// Set the row height, or how the row height is calculated
        /// </summary>
        public string RowHeight { get; set; } = "auto";

        /// <summary>
        /// Time delay before new data is requested when server-side processing is enabled
        /// </summary>
        public int ServerWait { get; set; } = 200;
    }
}
