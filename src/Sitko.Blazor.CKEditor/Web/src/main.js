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
    init: function (params) {
        window[params["editorClass"]]
            .create(document.querySelector('#' + params.selector), {})
            .then(editor => {
                window.SitkoBlazorCKEditor.editors[params.selector] = editor;
                editor.setData(params.content ? params.content : '');
                editor.model.document.on('change:data', () => {
                    params.instance.invokeMethodAsync('UpdateText', editor.getData());
                });
            })
            .catch(error => {
                console.error(error);
            });
    },
    destroy: function (params) {
        if (this.editors.hasOwnProperty(params.selector)) {
            this.editors[params.selector].destroy();
            delete this.editors[params.selector];
        }
    }
};
