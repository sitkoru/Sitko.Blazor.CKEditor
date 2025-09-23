namespace Sitko.Blazor.CKEditor.Bundle;

using System;
using System.Collections.Generic;

public class CKEditorBundleOptions : CKEditorOptions
{
    public CKEditorTheme Theme { get; set; } = CKEditorTheme.Light;

    public static CKEditorConfig DefaultConfig => new()
    {
        Toolbar = new CKEditorToolbar
        {
            Items = new List<string>
            {
                "undo",
                "redo",
                "|",
                "heading",
                "|",
                "bold",
                "italic",
                "strikethrough",
                "underline",
                "subscript",
                "superscript",
                "alignment",
                "|",
                "bulletedList",
                "numberedList",
                "|",
                "insertTable",
                "link",
                "code",
                "codeBlock",
                "horizontalLine",
                "specialCharacters",
                "|",
                "removeFormat"
            }
        },
        Language = "en"
    };

    public override string ScriptPath => $"{BasePath}/ckeditor.js";

    public override Dictionary<string, string> StylePaths
    {
        get
        {
            var styles = new Dictionary<string, string>();
            styles.Add("basic", $"{BasePath}/ckeditor.css");
            if (Theme == CKEditorTheme.Dark)
            {
                styles.Add("dark", $"{BasePath}/ckeditor.dark.css");
            }

            return styles;
        }
    }

    public override string EditorClassName { get; set; } = "BlazorEditor";
    private string BasePath => $"/_content/{typeof(CKEditorTheme).Assembly.GetName().Name}";
}

public enum CKEditorTheme
{
    Light,
    Dark
}
