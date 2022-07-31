using DataTables.Blazor.Abstractions;
using DataTables.Blazor.Options;
using DataTables.Blazor.Options.ExtensionsOptions;
using Microsoft.AspNetCore.Components; 

namespace DataTables.Blazor.Demo.Pages
{
    public partial class ExtensionsPage : ComponentBase
    { 

        private readonly DataTableOptions _buttonsDemoOptions = new DataTableOptions
        {
            DOM = "Bfrtip",
            ServerSide = true,
            ScrollY = "200", 
            DeferRender = true,
            Ordering =  false,
            Searching = false,
            Scroller = new ScrollerOptions()
            {
                LoadingIndicator = true,
                DisplayBuffer = "15"
            }
        }; 
    }
}
