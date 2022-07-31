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
    addEventListener: function (tableElement, eventName, dotNetCallback) {
        if (isEqual(eventName, "onrowclick")) {
            $(tableElement).on("click", "tbody tr", function (...args) {
                var row = $(tableElement).DataTable().row(this);
                var json = {
                    "id": String(row.id()),
                    "data": row.data()
                };
                return dotNetCallback.invokeMethodAsync("Invoke", json);
            });
        }
        else {
            var json = {};
            $(tableElement).on(eventName, function (...args) {
                json = argsToJson(args);

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
    } 
};

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