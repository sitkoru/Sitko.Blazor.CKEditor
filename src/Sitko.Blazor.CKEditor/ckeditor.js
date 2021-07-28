if (!window.SitkoBlazorCKEditor) {
  window.SitkoBlazorCKEditor = {
    editors: [],
    timeouts: [],
    init: function (element, editorClass, instance, id, configJson) {
      var config = {};
      if (configJson) {
        config = JSON.parse(configJson) ?? {};
      }
      //console.debug('CKEditor config', id, config);
      window[editorClass]
        .create(element, config)
        .then(editor => {
          window.SitkoBlazorCKEditor.editors[id] = editor;
          editor.model.document.on('change:data', () => {
            if (window.SitkoBlazorCKEditor.timeouts[id]) {
              clearTimeout(window.SitkoBlazorCKEditor.timeouts[id]);
            }
            window.SitkoBlazorCKEditor.timeouts[id] = setTimeout(function () {
              //console.debug(id, 'Update text');
              instance.invokeMethodAsync('UpdateText', editor.getData());
              delete window.SitkoBlazorCKEditor.timeouts[id];
            }, 50)

          });
        })
        .catch(error => {
          //console.error('Error initializing CKEditor', error);
        });
    },
    update: function (id, content) {
      if (this.editors.hasOwnProperty(id)) {
        var editor = this.editors[id];
        var oldData = editor.getData();
        if (oldData !== content) {
          //console.debug(id, 'Update value', oldData, content);
          editor.setData(content);
        }
      }
    },
    destroy: function (id) {
      if (this.editors.hasOwnProperty(id)) {
        //console.debug('Destroy editor');
        this.editors[id].destroy();
        delete this.editors[id];
      }
    }
  }
}
