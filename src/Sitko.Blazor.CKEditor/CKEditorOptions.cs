using System.ComponentModel.DataAnnotations;

namespace Sitko.Blazor.CKEditor
{
    using JetBrains.Annotations;

    [PublicAPI]
    public class CKEditorOptions
    {
        [Required] public virtual string ScriptPath { get; set; } = "";
        [Required] public virtual string EditorClassName { get; set; } = "";
        public virtual CKEditorConfig? CKEditorConfig { get; set; }
    }
}
