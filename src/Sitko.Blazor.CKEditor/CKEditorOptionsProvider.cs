using Microsoft.Extensions.Options;

namespace Sitko.Blazor.CKEditor
{
    public interface ICKEditorOptionsProvider
    {
        CKEditorOptions Options { get; }
    }

    public class CKEditorOptionsProvider<TOptions> : ICKEditorOptionsProvider where TOptions : CKEditorOptions
    {
        private readonly IOptionsMonitor<TOptions> optionsMonitor;
        public CKEditorOptions Options => optionsMonitor.CurrentValue;

        public CKEditorOptionsProvider(IOptionsMonitor<TOptions> optionsMonitor) => this.optionsMonitor = optionsMonitor;
    }
}
