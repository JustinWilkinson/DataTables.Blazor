# DataTables.Blazor
A basic port for jquery [DataTables](https://datatables.net/) into Blazor.

#### Disclaimer.
* This project is still under development, and needs a lot of work and is liable to be buggy.
* Its functionality is still very limited, as it does not yet fully support all options available in DataTables, in particular the callbacks etc are not yet supported.
* Interacting with the DataTables api is also not yet supported.
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
* At the moment there are two basic components, `DataTable` and `Column`.
* `Column` components should be nested inside the `DataTable` component.
* This is then transformed into a jquery DataTable configured with the DataTable defaults.
As is shown in the demo, adding a datatable is then as easy as adding the below to your page/component.
```html
<DataTable Id="MyTable" SourceUrl="sample-data/weather.json" Class="table table-striped table-bordered w-100">
    <Column Title="Date" Data="Date" />
    <Column Title="Temp. (C)" Data="TemperatureC" />
    <Column Title="Summary" ClassName="dt-body-center" Data="Summary" />
</DataTable>
```
* There is also an `Options` parameter on the DataTable which can expose more advanced configuration options.

## Code

### DataTables.Blazor
A Razor class library that brings the functionality of jquery Datatables into Blazor.

### DataTables.Blazor.Demo
A client-side Blazor WebAssembly application to demo the package.
