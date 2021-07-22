using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Sitko.Blazor.CKEditor
{
    using JetBrains.Annotations;

    [PublicAPI]
    public abstract class BaseCKEditorComponent : InputText, IAsyncDisposable
    {
        [Inject] protected ICKEditorOptionsProvider OptionsProvider { get; set; } = null!;
        [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;
        [Parameter] public string Placeholder { get; set; } = "Enter text";
        [Parameter] public string Class { get; set; } = "";
        [Parameter] public string Style { get; set; } = "";

        [Parameter] public CKEditorConfig? Config { get; set; }
        private IDisposable? instance;
        protected ElementReference EditorRef { get; set; }
        public Guid Id { get; } = Guid.NewGuid();
        private bool rendered;
        private string? lastValue;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                instance = DotNetObjectReference.Create(this);
                await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.loadScript",
                    OptionsProvider.Options.ScriptPath, OptionsProvider.Options.EditorClassName,
                    new {instance = DotNetObjectReference.Create(this), method = nameof(InitializeEditorAsync)});
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (rendered && lastValue != CurrentValue)
            {
                await UpdateEditor();
            }
        }

        [JSInvokable]
        public async Task InitializeEditorAsync()
        {
            await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.init", EditorRef,
                OptionsProvider.Options.EditorClassName, instance!, Id,
                Config ?? OptionsProvider.Options.CKEditorConfig);
            rendered = true;
            lastValue = CurrentValue;
        }


        protected ValueTask DestroyEditor()
        {
            rendered = false;
            return JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.destroy", Id);
        }

        [JSInvokable]
        public Task<bool> UpdateText(string editorText)
        {
            lastValue = editorText;
            CurrentValue = editorText;
            return Task.FromResult(true);
        }

        public ValueTask UpdateEditor() =>
            JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.update", Id, CurrentValue!);

        public ValueTask DisposeAsync()
        {
            instance?.Dispose();
            return DestroyEditor();
        }
    }
}
