namespace Sitko.Blazor.CKEditor;

using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

[PublicAPI]
public class CKEditorOptions
{
    [Required] public virtual string EditorClassName { get; set; } = "";
    public virtual CKEditorConfig? CKEditorConfig { get; set; }
}
