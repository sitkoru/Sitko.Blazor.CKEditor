namespace Sitko.Blazor.CKEditor;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

[PublicAPI]
public class CKEditorOptions
{
    [Required] public virtual string ScriptPath { get; set; } = "";
    [Required] public virtual string StylePath { get; set; } = "";
    public virtual Dictionary<string, string> GetAdditionalScripts(CKEditorConfig? config) => new();
    [Required] public virtual string EditorClassName { get; set; } = "";
    public virtual CKEditorConfig? CKEditorConfig { get; set; }
}
