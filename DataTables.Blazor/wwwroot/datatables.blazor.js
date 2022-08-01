window.datatablesInterop  = {
    initialiseDataTable: function(tableElement, options) {
        const opts = JSON.parse(options);

        if (opts.columns != null) {
            for (let i = 0; i < opts.columns.length; i++) {
                const col = opts.columns[i];
                col.createdCell = this.assignCallback(col.createdCell);
                col.render = this.assignCallback(col.render);
            }
        }

        if (opts.ajax != null) {
            opts.ajax.data = this.assignCallback(opts.ajax.data);
            opts.ajax.dataSrc = this.assignCallback(opts.ajax.data);
        }

        if (!$.fn.DataTable.isDataTable(tableElement) || opts.destroy) {
            $(tableElement).DataTable(opts);
        }
    },
    destroyDataTable: function (tableElement) { 
        if ($.fn.DataTable.isDataTable(tableElement)) {  
            $(tableElement).DataTable().destroy();

            // When a table is initialized with any given ScrollY parameter after 
            // the .destroy() command is called, the interface is not cleared of 
            // the destroyed table. It is not yet known why, so I manually remove
            // the table wrapper from the DOM
            tableId = null;

            if ($(tableElement).prop('id')) {
                tableId = $(tableElement).prop('id');
            }
            else if ($(tableElement).parent().parent().offsetParent().prop('id')) {
                tableId = $(tableElement).parent().parent().offsetParent().prop('id');
            }
            if (tableId) {
                wrapperId = '#' + tableId + (tableId.endsWith('_wrapper') ? '' : '_wrapper');
                if ($(wrapperId)) {
                    $(wrapperId).remove();
                }
            }
        }
    },
    ajaxReloadDataTable: function (tableElement) {
        $(tableElement).DataTable().ajax.reload().draw();
    },
    reloadDataTable: function (tableElement, jsonData) {
        const data = JSON.parse(jsonData);
        $(tableElement).DataTable().clear().rows.add(data).draw();
    },
    assignCallback: function (callback) {
        if (callback == null || callback.namespace == null || callback.function == null) {
            return undefined;
        }

        const namespace = window[callback.namespace];
        if (namespace === null || namespace === undefined) {
            return undefined;
        }

        const func = namespace[callback.function];
        if (typeof func === 'function') {
            return func;
        } else {
            return undefined;
        }
    }
};