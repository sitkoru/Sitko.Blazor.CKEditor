namespace Sitko.Blazor.CKEditor;

using System.Collections.Generic;
using System.Linq;


public class CKEditorConfig
{
    public string? InitialData { get; set; }
    public string? Language { get; set; }
    public string? Placeholder { get; set; }
    public CKEditorToolbar? Toolbar { get; set; }
}

public class CKEditorToolbar
{
    public List<string> Items { get; set; } = new();

    public CKEditorToolbar AppendItem(string item) => AppendItems(new[] { item });

    public CKEditorToolbar AppendItems(IEnumerable<string> items)
    {
        Items.AddRange(items);
        return this;
    }

    public CKEditorToolbar PrependItem(string item) => PrependItems(new[] { item });

    public CKEditorToolbar PrependItems(IEnumerable<string> items)
    {
        Items.Reverse();
        var list = items.Reverse();
        AppendItems(list);
        Items.Reverse();
        return this;
    }
}
