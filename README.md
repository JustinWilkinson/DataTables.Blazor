# DataTables.Blazor
A basic port for jquery [DataTables](https://datatables.net/) into Blazor.

#### About:
This was written purely for fun, if you'd like to use it you're welcome to do so, it's under an [MIT License](https://github.com/JustinWilkinson/DataTables.Blazor/blob/master/LICENSE).
If you've found it useful, and want to say thanks, feel free to buy me a coffee, otherwise enjoy.  
 [!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/justinwilkinson)

#### Disclaimer.
* This project is still pretty immature, and needs a lot of work and is liable to be buggy.
* Its functionality may not yet fully replicate all the options available in DataTables.
* However, DataTables comes with a lot of great features out of the box which can be easily utilised straight away.

#### Setup:
##### Prerequisites:
You will need to include jquery Datatables in your project, some of the options are not available to older versions so to be on the safe side, v1.10.24 would be best.
Steps to complete can be found [here](https://datatables.net/manual/installation).

##### Configuration:
Below are the steps required to configure the package in a client-side Blazor WebAssembly app.

* Install the `DataTables.Blazor` package using your preferred method.
* Add the following using statement to the top of your `Program.cs`:
```csharp
using DataTables.Blazor.Extensions;
```
* Then add the following line to add the necessary services to your `Program.cs`.
```csharp
builder.Services.AddDataTables();
```
* Add the following line to your `index.html`, below where you include DataTables.
```html
<script src="_content/DataTables.Blazor/datatables.blazor.min.js"></script>
```
* Finally, add the following statement to your `_Imports.razor`
```csharp
@using DataTables.Blazor
```
You should now be ready to get started with DataTables.Blazor!

#### Usage:
* At the moment there are two basic components, `DataTable` and `DataTableColumn`.
* `DataTableColumn` components should be nested inside the `DataTable` component.
* This is then transformed into a jquery DataTable configured with the DataTable defaults.
As is shown in the demo, adding a datatable is then as easy as adding the below to your page/component.
```html
<DataTable SourceUrl="sample-data/weather.json" Class="table table-striped table-bordered w-100">
    <DataTableColumn Title="Date" Data="Date" />
    <DataTableColumn Title="Temp. (C)" Data="TemperatureC" />
    <DataTableColumn Title="Summary" ClassName="dt-body-center" Data="Summary" />
</DataTable>
```
* There is also an `Options` parameter on the DataTable which can expose more advanced configuration options.

## Code

### DataTables.Blazor
A Razor class library that brings the functionality of jquery Datatables into Blazor.

### DataTables.Blazor.Demo
A client-side Blazor WebAssembly application to demo the package.

### DataTables.Blazor.Tests
Xunit/Bunit tests for the DataTables.Blazor project.


## Release Notes:
### 3.2.0
* Add reload and reinitialize methods as well as `AutoReload` for the `DataTable` component.

### 3.1.0
* Add support for datatables `Button` extension.

### 3.0.0
* Rename `Column` component to `DataTableColumn` to reduce risk of naming clash.

### 2.1.0
* Added `Data` attribute to `DataTable` and `Dataset<T>` to allow specifying a C# object as a data source.

### 2.0.0
* Update packages for .NET 5 and add .NET 6 target.

### 1.0.0
* Add `JavaScriptFunction` to allow JS callbacks to be set where available in jQuery datatables.

### 0.x.x
* Early implementations with little functionality.
