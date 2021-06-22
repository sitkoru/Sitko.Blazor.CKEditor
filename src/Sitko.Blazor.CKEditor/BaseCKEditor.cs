using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Sitko.Blazor.CKEditor
{
    public abstract class BaseCKEditorComponent : InputText, IAsyncDisposable
    {
        [Inject] protected IOptions<CKEditorOptions> Options { get; set; } = null!;
        [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;
        protected string EditorId { get; } = $"cke_{Guid.NewGuid()}";
        protected bool HtmlMode;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.loadScript",
                    new
                    {
                        scriptPath = Options.Value.ScriptPath,
                        callback = new
                        {
                            instance = DotNetObjectReference.Create(this),
                            method = nameof(InitializeEditorAsync)
                        }
                    });
            }
        }

        [JSInvokable]
        public async Task InitializeEditorAsync()
        {
            var arg = new
            {
                selector = $"{EditorId}",
                editorClass = Options.Value.EditorClassName,
                instance = DotNetObjectReference.Create(this)
            };
            await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.init", arg);
        }

        private ValueTask DestroyEditor()
        {
            if (!HtmlMode)
            {
                var arg = new {selector = $"{EditorId}"};
                return JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.destroy", arg);
            }

            return new ValueTask();
        }

        [JSInvokable]
        public Task<bool> UpdateText(string editorText)
        {
            CurrentValue = editorText;
            return Task.FromResult(true);
        }

        public ValueTask DisposeAsync()
        {
            return DestroyEditor();
        }

        protected async Task SwitchModeAsync()
        {
            if (HtmlMode)
            {
                HtmlMode = false;
                await InitializeEditorAsync();
            }
            else
            {
                await DestroyEditor();
                HtmlMode = true;
            }
        }

        [Parameter] public bool AllowHtmlMode { get; set; } = true;
    }
}
