window.datatablesInterop  = {
    initialiseDataTable: function(tableElement, options) {
        const opts = JSON.parse(options);

        for (let i = 0; i < opts.columns.length; i++) {
            const col = opts.columns[i];
            col.createdCell = this.assignCallback(col.createdCell);
            col.render = this.assignCallback(col.render);
        }

        $(tableElement).DataTable(opts);
    },
    destroyDataTable: function (tableElement) {
        $(tableElement).DataTable().destroy();
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