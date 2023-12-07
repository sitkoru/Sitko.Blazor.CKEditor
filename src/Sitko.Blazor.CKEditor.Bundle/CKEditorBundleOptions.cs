namespace Sitko.Blazor.CKEditor.Bundle;

using System.Collections.Generic;
using JetBrains.Annotations;

[PublicAPI]
public class CKEditorBundleOptions : CKEditorOptions
{
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

    public override string EditorClassName { get; set; } = "BlazorEditor";
}
