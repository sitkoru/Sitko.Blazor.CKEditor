# Sitko.Blazor.CKEditor

![Nuget](https://img.shields.io/nuget/dt/Sitko.Blazor.CKEditor) ![Nuget](https://img.shields.io/nuget/v/Sitko.Blazor.CKEditor)

CKEditor component for Blazor Applications

# Installation

```
dotnet add package Sitko.Blazor.CKEditor
```

Register in DI and configure in `Program.cs`

```c#
builder.Services.AddCKEditor(builder.Configuration);
```

and `appsettings.json`:

```json
{
    "CKEditor": {
        "ScriptPath": "https://cdn.ckeditor.com/ckeditor5/28.0.0/classic/ckeditor.js",
        "EditorClassName": "ClassicEditor"
    }
}
```

Or in code:

```c#
services.AddCKEditor(Configuration, options =>
{
    options.ScriptPath = "https://cdn.ckeditor.com/ckeditor5/28.0.0/classic/ckeditor.js";
    options.EditorClassName = "ClassicEditor";
});
```

If you have custom build or separate css file - configure it via StylePath:

```json
{
    "CKEditor": {
        "ScriptPath": "/ckeditor/ckeditor.js",
        "StylePath": "/ckeditor/ckeditor.css",
        "EditorClassName": "ClassicEditor"
    }
}
```
```c#
services.AddCKEditor(Configuration, options =>
{
    options.ScriptPath = "/ckeditor/ckeditor.js";
    options.StylePath = "/ckeditor/ckeditor.css";
    options.EditorClassName = "ClassicEditor";
});
```

**It is recommended to use separate css file with new blazor navigation and Auto render mode.** 

Add to `App.razor`

```html
<script src="_content/Sitko.Blazor.CKEditor/ckeditor.js"></script>
```

**Don't forget to link ckeditor js/css files**

Add to `_Imports.razor`

```c#
@using Sitko.Blazor.CKEditor
```

# Usage

```c#
<CKEditor @bind-Value="@Model.Field"></CKEditor>
```

# Sitko.Blazor.CKEditor.Bundle

![Nuget](https://img.shields.io/nuget/dt/Sitko.Blazor.CKEditor.Bundle) ![Nuget](https://img.shields.io/nuget/v/Sitko.Blazor.CKEditor.Bundle)

This package includes basic ckeditor build with light and dark themes. Install:

```
dotnet add package Sitko.Blazor.CKEditor.Bundle
```

Instead of `AddCKEditor` use:

```c#
builder.Services.AddCKEditorBundle(builder.Configuration);
```

and to `appsettings.json` 

```json
{
    "CKEditorBundle": {
        "Theme": "Dark"
    }
}
```
