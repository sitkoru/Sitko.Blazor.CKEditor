using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;


namespace Sitko.Blazor.CKEditor
{
    [PublicAPI]
    public class CKEditorConfig
    {
        public string? InitialData { get; set; }
        public string Language { get; set; } = "en";
        public string? Placeholder { get; set; }
        public CKEditorToolbar Toolbar { get; set; } = new();
    }

    [PublicAPI]
    public class CKEditorToolbar
    {
        public List<string> Items { get; set; } = new();

        public CKEditorToolbar AppendItem(string item) => AppendItems(new[] {item});

        public CKEditorToolbar AppendItems(IEnumerable<string> items)
        {
            Items.AddRange(items);
            return this;
        }

        public CKEditorToolbar PrependItem(string item) => PrependItems(new[] {item});

        public CKEditorToolbar PrependItems(IEnumerable<string> items)
        {
            Items.Reverse();
            var list = items.Reverse();
            AppendItems(list);
            Items.Reverse();
            return this;
        }
    }
}
