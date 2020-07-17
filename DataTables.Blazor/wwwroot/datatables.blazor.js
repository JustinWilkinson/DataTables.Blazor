window.datatablesInterop = {
    initialiseDataTable: (id, options) => { $(`#${id}`).DataTable(JSON.parse(options)); }
};
