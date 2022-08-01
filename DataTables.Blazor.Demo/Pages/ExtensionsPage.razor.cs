
using DataTables.Blazor.Abstractions;
using DataTables.Blazor.Options;
using DataTables.Blazor.Options.ExtensionsOptions;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace DataTables.Blazor.Demo.Pages
{
    public partial class ExtensionsPage : ComponentBase
    { 
        DataTableOptions scrollerDemoOptions = new DataTableOptions
        {
            ServerSide = true,
            DeferRender = true,
            ScrollY = "200",
            Ordering = false,
            Searching = false,
            Scroller = new ScrollerOptions()
            {
                LoadingIndicator = true,
                DisplayBuffer = "15"
            }
        };  
    }
}
