using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Sitko.Blazor.CKEditor
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.Extensions.Logging;
    using ScriptInjector;

    [PublicAPI]
    public abstract class BaseCKEditorComponent : InputText, IAsyncDisposable
    {
        private IDisposable? instance;
        private bool rendered;
        protected string EditorValue { get; private set; } = "";
        [Inject] protected ICKEditorOptionsProvider OptionsProvider { get; set; } = null!;
        [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;
        [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;
        [Inject] protected ILogger<BaseCKEditorComponent> Logger { get; set; } = null!;
        [Parameter] public string Placeholder { get; set; } = "Enter text";
        [Parameter] public string Class { get; set; } = "";
        [Parameter] public string Style { get; set; } = "";

        [Parameter] public CKEditorConfig? Config { get; set; }

        protected ElementReference EditorRef { get; set; }
        public Guid Id { get; } = Guid.NewGuid();

        public ValueTask DisposeAsync()
        {
            instance?.Dispose();
            return DestroyEditor();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (Value != EditorValue)
            {
                EditorValue = Value ?? "";
                if (rendered)
                {
                    await UpdateEditorAsync();
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                instance = DotNetObjectReference.Create(this);
                var scripts = new List<ScriptInjectRequest>
                {
                    ScriptInjectRequest.FromResource("BlazorCkEditor", GetType().Assembly, "ckeditor.js"),
                    ScriptInjectRequest.FromUrl(OptionsProvider.Options.EditorClassName,
                        OptionsProvider.Options.ScriptPath)
                };
                var config = GetConfig();
                foreach (var (key, path) in OptionsProvider.Options.GetAdditionalScripts(config))
                {
                    scripts.Add(ScriptInjectRequest.FromUrl(key, path));
                }

                await ScriptInjector.InjectAsync(scripts, InitializeEditorAsync);
            }
        }

        private async Task InitializeEditorAsync(CancellationToken cancellationToken)
        {
            await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.init", cancellationToken, EditorRef,
                OptionsProvider.Options.EditorClassName, instance!, Id,
                JsonSerializer.Serialize(GetConfig(),
                    new JsonSerializerOptions
                    {
                        IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }));
            rendered = true;
        }

        private CKEditorConfig? GetConfig() => Config ?? OptionsProvider.Options.CKEditorConfig;


        protected ValueTask DestroyEditor()
        {
            rendered = false;
            return JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.destroy", Id);
        }

        [JSInvokable]
        public async Task<bool> UpdateText(string editorText)
        {
            if (EditorValue != editorText)
            {
                EditorValue = editorText;
                await ValueChanged.InvokeAsync(EditorValue);
            }

            return true;
        }

        private async ValueTask UpdateEditorAsync() =>
            await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.update", Id, EditorValue);
    }
}
