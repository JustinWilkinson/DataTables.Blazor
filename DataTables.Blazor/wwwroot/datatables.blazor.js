window.datatablesInterop = {
    initialiseDataTable: (tableElement, options) => {
        $(tableElement).DataTable(JSON.parse(options));
    }
};
