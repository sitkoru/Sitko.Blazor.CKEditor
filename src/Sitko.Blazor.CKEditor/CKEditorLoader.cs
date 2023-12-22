namespace Sitko.Blazor.CKEditor;

using System.Collections.Generic;
using System.Threading.Tasks;
using ScriptInjector;

class CKEditorLoader : ICKEditorLoader
{
    private TaskCompletionSource? loadTaskSource;
    private readonly IScriptInjector scriptInjector;
    private readonly ICKEditorOptionsProvider optionsProvider;

    public CKEditorLoader(IScriptInjector scriptInjector, ICKEditorOptionsProvider optionsProvider)
    {
        this.scriptInjector = scriptInjector;
        this.optionsProvider = optionsProvider;
    }

    public Task LoadAsync()
    {
        if (loadTaskSource is null)
        {
            loadTaskSource = new TaskCompletionSource();
            var scripts = new List<InjectRequest>
            {
                ScriptInjectRequest.FromResource("BlazorCkEditor", GetType().Assembly, "ckeditor.js"),
                ScriptInjectRequest.FromUrl(optionsProvider.Options.EditorClassName,
                    optionsProvider.Options.ScriptPath),
            };
            return scriptInjector.InjectAsync(scripts, _ =>
            {
                loadTaskSource.SetResult();
                return Task.CompletedTask;
            });
        }

        return loadTaskSource.Task;
    }
}
