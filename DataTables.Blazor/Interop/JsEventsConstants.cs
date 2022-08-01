using DataTables.Blazor.Abstractions;
using DataTables.Blazor.Abstractions.JsonConverters;
using DataTables.Blazor.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataTables.Blazor.Interop
{
    /// <summary>
    /// DataTables Events interop constants
    /// </summary>
    public static class JsEventsConstants
    {
        #region "Custom"

        /// <summary>
        /// On row click.
        /// </summary>
        public const string OnRowClick = "onrowclick";

        /// <summary>
        /// On cell click.
        /// </summary>
        public const string OnCellClick = "oncellclick";

        #endregion

        #region "DataTables Group"

        /// <summary>
        /// A child row has been added or removed from the table.
        /// </summary>
        public const string ChildRow = "childRow.dt";

        /// <summary>
        /// Column sizing event - fired when the column widths are recalculated.
        /// </summary>
        public const string ColumnSizing = "column-sizing.dt";

        /// <summary>
        /// Column visibility event - fired when the visibility of a column changes.
        /// </summary>
        public const string ColumnVisibility = "column-visibility.dt";

        /// <summary>
        /// Table destroy event - fired when a table is destroyed.
        /// </summary>
        public const string Destroy = "destroy.dt";

        /// <summary>
        /// Draw event - fired once the table has completed a draw.
        /// </summary>
        public const string Draw = "draw.dt";

        /// <summary>
        /// Error event - An error has occurred during DataTables' processing of data.
        /// </summary>
        public const string Error = "error.dt";

        /// <summary>
        /// Initialisation complete event - fired when DataTables has been fully initialised and data loaded.
        /// </summary>
        public const string Init = "init.dt";

        /// <summary>
        ///Page length change event - fired when the page length is changed.
        /// </summary>
        public const string Length = "length.dt";

        /// <summary>
        /// Order event - fired when the data contained in the table is ordered.
        /// </summary>
        public const string Order = "order.dt";

        /// <summary>
        /// Page change event - fired when the table's paging is updated.
        /// </summary>
        public const string Page = "page.dt";

        /// <summary>
        /// Pre-draw event - triggered as the table is about to be redrawn.
        /// </summary>
        public const string PreDraw = "preDraw.dt";

        /// <summary>
        /// Initialisation started event - triggered immediately before data load.
        /// </summary>
        public const string PreInit = "preInit.dt";

        /// <summary>
        /// Ajax event - fired before an Ajax request is made
        /// </summary>
        public const string PreXhr = "preXhr.dt";

        /// <summary>
        /// Processing event - fired when DataTables is processing data
        /// </summary>
        public const string Processing = "processing.dt";

        /// <summary>
        /// DataTables wants to display a child row
        /// </summary>
        public const string RequestChild = "requestChild.dt";

        /// <summary>
        /// Search event - fired when the table is filtered.
        /// </summary>
        public const string Search = "search.dt";

        /// <summary>
        /// State load event - fired when loading state from storage.
        /// </summary>
        public const string StateLoadParams = "stateLoadParams.dt";

        /// <summary>
        /// State loaded event - fired once state has been loaded and applied.
        /// </summary>
        public const string StateLoaded = "stateLoaded.dt";

        /// <summary>
        /// State save event - fired when saving table state information.
        /// </summary>
        public const string StateSaveParams = "stateSaveParams.dt";

        /// <summary>
        /// Ajax event - fired when an Ajax request is completed
        /// </summary>
        public const string Xhr = "xhr.dt";

        #endregion

        #region "AutoFill Group"

        // TODO: Write constants for AutoFill group

        #endregion

        #region "Buttons Group"

        // TODO: Write constants for Buttons group

        #endregion

        // TODO: Write constants for other groups
    }
}