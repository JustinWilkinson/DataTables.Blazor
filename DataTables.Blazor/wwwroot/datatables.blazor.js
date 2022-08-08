window.datatablesInterop = {
    initialiseDataTable: function (tableElement, options) {
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
            else if ($(tableElement).closest('.dataTables_wrapper').prop('id')) {
                tableId = $(tableElement).closest('.dataTables_wrapper').prop('id');
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
    }, 
    addEventListener: function (tableElement, eventName, dotNetCallback) {
        if (isEqual(eventName, "onrowclick")) {
            addRowClickListener(tableElement, dotNetCallback);
        }
        else if (isEqual(eventName, "oncellclick")) {
            addCellClickListener(tableElement, dotNetCallback);
        }
        else {
            addGenericListener(tableElement, eventName, dotNetCallback);
        }
    } 
};

function addRowClickListener(tableElement, dotNetCallback) {
    $(tableElement).on("click", "tbody tr", function (...args) {
        var row = $(tableElement).DataTable().row(this);
        var json = {
            "index": row.index(),
            "id": String(row.id()),
            "data": row.data()
        };
        return dotNetCallback.invokeMethodAsync("Invoke", json);
    });
}

function addCellClickListener(tableElement, dotNetCallback) {
    $(tableElement).on("click", "tbody td", function (...args) {
        var table = $(tableElement).DataTable();
        var cell = table.cell(this);
        var row = table.row($(this).closest('tr'));
        var json = {
            "columnIndex": cell.index().column,
            "rowIndex": cell.index().row,
            "rowId": String(row.id()),
            "data": cell.data()
        };
        return dotNetCallback.invokeMethodAsync("Invoke", json);
    });
}

function addGenericListener(tableElement, eventName, dotNetCallback) {
    var json = {};
    $(tableElement).on(eventName, function (...args) {
        json = argsToJson(args);

        // prepare custom json args for page event
        if (isEqual(eventName, "page.dt")) { 
            var info = $(tableElement).DataTable().page.info();
            json = {
                "page": info["page"],
                "pages": info["pages"],
                "start": info["start"],
                "end": info["end"],
                "length": info["length"]
            }
        }

        return dotNetCallback.invokeMethodAsync("Invoke", json);
    });
}
function argsToJson(_obj) {
    var names = [],
        args = arguments,
        len = function (o) {
            var a = [];

            for (var i in o) a.push(i);

            return a;
        };

    for (var i in _obj) {
        names.push(i);
    }

    var jsonData = {};

    [].forEach.call(len(_obj), function (a, b) {
        if (!isEqual(names[b], "e") &&
            !isEqual(names[b], "settings") &&
            !isEqual(names[b], "dt") &&
            !isEqual(names[b], "datatable") &&
            (args[0][names[b]] instanceof Object) == false) {
            jsonData[names[b]] = args[0][names[b]];
        }
    });

    return jsonData;
}
function isEqual(str1, str2) {
    return str1.normalize() === str2.normalize();
}