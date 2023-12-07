namespace Sitko.Blazor.CKEditor;

using Microsoft.Extensions.Options;

public interface ICKEditorOptionsProvider
{
    CKEditorOptions Options { get; }
}

public class CKEditorOptionsProvider<TOptions> : ICKEditorOptionsProvider where TOptions : CKEditorOptions
{
    private readonly IOptionsMonitor<TOptions> optionsMonitor;

    public CKEditorOptionsProvider(IOptionsMonitor<TOptions> optionsMonitor) =>
        this.optionsMonitor = optionsMonitor;

    public CKEditorOptions Options => optionsMonitor.CurrentValue;
}
