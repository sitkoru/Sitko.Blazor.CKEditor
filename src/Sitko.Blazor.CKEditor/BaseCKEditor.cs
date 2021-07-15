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
        [Inject] protected ICKEditorOptionsProvider OptionsProvider { get; set; } = null!;
        [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;
        [Parameter] public string Placeholder { get; set; } = "Enter text";
        [Parameter] public string Class { get; set; } = "";
        [Parameter] public string Style { get; set; } = "";

        [Parameter]
        public string TextareaStyle { get; set; } = "width: 100%; " +
                                                    "border: 1px solid var(--ck-color-base-border); " +
                                                    "border-radius: var(--ck-border-radius); " +
                                                    "padding: var(--ck-spacing-large) var(--ck-spacing-standard);";

        [Parameter] public string TextareaClass { get; set; } = "";
        [Parameter] public string SwitchToEditorText { get; set; } = "Switch to editor";
        [Parameter] public string SwitchToHtmlText { get; set; } = "Switch to HTML";
        [Parameter] public bool AllowHtmlMode { get; set; } = true;
        [Parameter] public RenderFragment<CKEdtitorModeSwitcher>? ModeSwitcherContent { get; set; }
        private CKEditorMode _mode = CKEditorMode.Editor;
        private IDisposable? _instance;
        protected ElementReference EditorRef { get; set; }
        protected readonly CKEdtitorModeSwitcher ModeSwitcher;
        public readonly Guid Id = Guid.NewGuid();
        private bool _rendered;
        private string? _lastValue;

        public BaseCKEditorComponent()
        {
            ModeSwitcher = new CKEdtitorModeSwitcher(this, _mode);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                _instance = DotNetObjectReference.Create(this);
                await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.loadScript",
                    OptionsProvider.Options.ScriptPath, OptionsProvider.Options.EditorClassName,
                    new {instance = DotNetObjectReference.Create(this), method = nameof(InitializeEditorAsync)});
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (_rendered && _lastValue != CurrentValue)
            {
                await UpdateEditor();
            }
        }

        [JSInvokable]
        public async Task InitializeEditorAsync()
        {
            await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.init", EditorRef,
                OptionsProvider.Options.EditorClassName, _instance!, Id);
            _rendered = true;
            _lastValue = CurrentValue;
        }

        protected ValueTask DestroyEditor()
        {
            _rendered = false;
            if (_mode == CKEditorMode.Editor)
            {
                return JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.destroy", Id);
            }

            return new ValueTask();
        }

        [JSInvokable]
        public Task<bool> UpdateText(string editorText)
        {
            _lastValue = editorText;
            CurrentValue = editorText;
            return Task.FromResult(true);
        }

        public ValueTask UpdateEditor()
        {
            return JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.update", Id, CurrentValue!);
        }

        public ValueTask DisposeAsync()
        {
            _instance?.Dispose();
            return DestroyEditor();
        }

        public async Task<CKEditorMode> SwitchModeAsync()
        {
            if (_mode == CKEditorMode.Html)
            {
                _mode = CKEditorMode.Editor;
                await InitializeEditorAsync();
            }
            else
            {
                await DestroyEditor();
                _mode = CKEditorMode.Html;
            }

            return _mode;
        }
    }

    public class CKEdtitorModeSwitcher
    {
        private readonly BaseCKEditorComponent _editor;
        public CKEditorMode CurrentMode { get; private set; }

        public CKEdtitorModeSwitcher(BaseCKEditorComponent editor, CKEditorMode mode)
        {
            _editor = editor;
            CurrentMode = mode;
        }

        public async Task SwitchModeAsync()
        {
            CurrentMode = await _editor.SwitchModeAsync();
        }
    }

    public enum CKEditorMode
    {
        Editor,
        Html
    }
}
