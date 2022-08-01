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
    },
    addRowClass: function (tableElement, rowIndex, className) {
        if ($(tableElement).DataTable()) {
            var rowByIndex = $(tableElement).DataTable().row(rowIndex);
            $(rowByIndex.node()).addClass(className);
        }
    },
    removeRowClass: function (tableElement, rowIndex, className) {
        if ($(tableElement).DataTable()) {
            var rowByIndex = $(tableElement).DataTable().row(rowIndex);
            $(rowByIndex.node()).removeClass(className);
        }
    },
    addColumnClass: function (tableElement, columnIndex, className, header = false) {
        if ($(tableElement).DataTable()) {
            var columnByIndex = $(tableElement).DataTable().column(columnIndex, { order: 'index' });
            $(columnByIndex.nodes()).addClass(className);

            if (header == true) {
                this.addHeaderClass(tableElement, columnIndex, className);
            }
        }
    },
    removeColumnClass: function (tableElement, columnIndex, className, header = false) {
        if ($(tableElement).DataTable()) {
            var columnByIndex = $(tableElement).DataTable().column(columnIndex, { order: 'index' });
            $(columnByIndex.nodes()).removeClass(className);

            if (header == true) {
                this.removeHeaderClass(tableElement, columnIndex, className);
            }
        }
    },
    addHeaderClass: function (tableElement, columnIndex, className) {
        if ($(tableElement).DataTable()) {
            var columnByIndex = $(tableElement).DataTable().column(columnIndex, { order: 'index' });
            $(columnByIndex.header()).addClass(className);
        }
    },
    removeHeaderClass: function (tableElement, columnIndex, className) {
        if ($(tableElement).DataTable()) {
            var columnByIndex = $(tableElement).DataTable().column(columnIndex, { order: 'index' });
            $(columnByIndex.header()).removeClass(className);
        }
    }
};