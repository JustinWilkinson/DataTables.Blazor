window.datatablesInterop = {
    initialiseDataTable: (id, options) => { console.log(options); $(`#${id}`).DataTable(options); }
};
