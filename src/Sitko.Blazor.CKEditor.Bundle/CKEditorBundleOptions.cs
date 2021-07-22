namespace Sitko.Blazor.CKEditor.Bundle
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    [PublicAPI]
    public class CKEditorBundleOptions : CKEditorOptions
    {
        public CKEditorTheme Theme { get; set; } = CKEditorTheme.Light;

        public static CKEditorConfig DefaultConfig => new()
        {
            Toolbar = new CKEditorToolbar
            {
                Items = new List<string>
                {
                    "heading",
                    "|",
                    "bold",
                    "italic",
                    "link",
                    "bulletedList",
                    "numberedList",
                    "undo",
                    "redo"
                }
            }
        };

        public override string ScriptPath => Theme switch
        {
            CKEditorTheme.Light => $"{BasePath}/ckeditor.js",
            CKEditorTheme.Dark => $"{BasePath}/ckeditor.dark.js",
            _ => throw new ArgumentOutOfRangeException()
        };

        public override string EditorClassName { get; set; } = "BlazorEditor";
        private string BasePath => $"/_content/{typeof(CKEditorTheme).Assembly.GetName().Name}";
    }

    public enum CKEditorTheme
    {
        Light,
        Dark
    }
}
