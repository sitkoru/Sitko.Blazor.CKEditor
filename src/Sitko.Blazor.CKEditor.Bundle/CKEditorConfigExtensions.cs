namespace Sitko.Blazor.CKEditor.Bundle
{
    using JetBrains.Annotations;

    [PublicAPI]
    public static class CKEditorConfigExtensions
    {
        public static CKEditorConfig WithHtmlEditing(this CKEditorConfig config)
        {
            config.Toolbar.PrependItems(new[] {"sourceEditing", "|"});
            return config;
        }
    }
}
