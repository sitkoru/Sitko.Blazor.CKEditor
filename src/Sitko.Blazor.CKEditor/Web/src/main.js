window.SitkoBlazorCKEditor = {
    loaded: [],
    loading: [],
    editors: [],
    loadScript: function (scriptArgs) {
        // check list - if already loaded we can ignore
        const scriptPath = scriptArgs.scriptPath;
        if (this.loaded[scriptPath]) {
            console.debug(scriptPath + " already loaded");
            // return 'empty' promise
            return new Promise(function (resolve, reject) {
                resolve();
                if (scriptArgs.callback) {
                    scriptArgs.callback.instance.invokeMethodAsync(scriptArgs.callback.method, scriptArgs.callback.data);
                }
            });
        }

        if (this.loading[scriptPath]) {
            console.debug(scriptPath + " loading");
            // return current promise
            return this.loading[scriptPath].then(() => {
                if (scriptArgs.callback) {
                    scriptArgs.callback.instance.invokeMethodAsync(scriptArgs.callback.method, scriptArgs.callback.data);
                }
            });
        }

        this.loading[scriptPath] = new Promise(function (resolve, reject) {
            // create JS library script element
            const script = document.createElement("script");
            script.src = scriptPath;
            script.type = "text/javascript";
            console.debug(scriptPath + " created");

            // if the script returns okay, return resolve
            script.onload = function () {
                console.debug(scriptPath + " loaded ok");
                resolve(scriptPath);
                if (scriptArgs.callback) {
                    scriptArgs.callback.instance.invokeMethodAsync(scriptArgs.callback.method, scriptArgs.callback.data);
                }
                // flag as loaded
                window.SitkoBlazorCKEditor.loaded[scriptPath] = true;
            };

            // if it fails, return reject
            script.onerror = function () {
                console.debug(scriptPath + " load failed");
                reject(scriptPath);
            }

            // scripts will load at end of body
            document["body"].appendChild(script);
        });
    },
    init: function (element, editorClass, instance, id) {
        window[editorClass]
            .create(element, {})
            .then(editor => {
                window.SitkoBlazorCKEditor.editors[id] = editor;
                editor.model.document.on('change:data', () => {
                    instance.invokeMethodAsync('UpdateText', editor.getData());
                });
            })
            .catch(error => {
                console.error(error);
            });
    },
    update: function (id, content) {
        if (this.editors.hasOwnProperty(id)) {
            this.editors[id].setData(content);
        }
    },
    destroy: function (id) {
        if (this.editors.hasOwnProperty(id)) {
            this.editors[id].destroy();
            delete this.editors[id];
        }
    }
};
