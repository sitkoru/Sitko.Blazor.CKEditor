namespace Sitko.Blazor.CKEditor.Bundle;


public static class CKEditorConfigExtensions
{
    public static CKEditorConfig WithHtmlEditing(this CKEditorConfig config)
    {
        config.Toolbar ??= new CKEditorToolbar();

        config.Toolbar.PrependItems(new[] { "sourceEditing", "|" });

        return config;
    }
}
