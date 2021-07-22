window.SitkoBlazorCKEditor = {
  loaded: [],
  loading: [],
  editors: [],
  loadScript: function (scriptPath, scriptKey, callback) {
    // check list - if already loaded we can ignore
    scriptKey ??= scriptPath;
    if (this.loaded[scriptKey]) {
      console.debug(scriptKey + " already loaded");
      if (scriptPath !== this.loaded[scriptKey]) {
        console.debug(scriptKey + " path changed");
      }
      // return 'empty' promise
      return new Promise(function (resolve) {
        resolve();
        if (callback) {
          callback.instance.invokeMethodAsync(callback.method, callback.data);
        }
      });
    }

    if (this.loading[scriptKey]) {
      console.debug(scriptKey + " loading");
      // return current promise
      return this.loading[scriptKey].then(() => {
        if (callback) {
          callback.instance.invokeMethodAsync(callback.method, callback.data);
        }
      });
    }

    this.loading[scriptKey] = new Promise(function (resolve, reject) {
      // create JS library script element
      const script = document.createElement("script");
      script.src = scriptPath;
      script.type = "text/javascript";
      script.id = scriptKey;
      console.debug(scriptKey + " start loading");

      // if the script returns okay, return resolve
      script.onload = function () {
        console.debug(scriptKey + " loaded ok");
        resolve(scriptPath);
        if (callback) {
          callback.instance.invokeMethodAsync(callback.method, callback.data);
        }
        // flag as loaded
        window.SitkoBlazorCKEditor.loaded[scriptKey] = scriptPath;
        delete window.SitkoBlazorCKEditor.loading[scriptKey];
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
  init: function (element, editorClass, instance, id, configJson) {
    let config = {};
    if (configJson) {
      config = JSON.parse(configJson);
    }
    console.debug('CKEditor config', config);
    window[editorClass]
      .create(element, config)
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
