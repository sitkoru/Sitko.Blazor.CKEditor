# Sitko.Blazor.CKEditor

![Nuget](https://img.shields.io/nuget/dt/Sitko.Blazor.CKEditor) ![Nuget](https://img.shields.io/nuget/v/Sitko.Blazor.CKEditor)

CKEditor component for Blazor Applications

# Installation

```
dotnet add package Sitko.Blazor.CKEditor
```

Register in DI and configure in `Startup.cs`

```c#
services.AddCKEditor(Configuration);
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

Add to `_Host.cshtml`

```c#
<script src="_content/Sitko.Blazor.CKEditor/Sitko.Blazor.CKEditor.js"></script>
```

before `_framework/blazor.server.js`

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
services.AddCKEditorBundle(Configuration);
```

and `appsettings.json`:

```json
{
    "CKEditorBundle": {
        "Theme": "Dark"
    }
}
```
