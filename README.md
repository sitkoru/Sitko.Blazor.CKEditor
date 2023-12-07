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
        "EditorClassName": "ClassicEditor"
    }
}
```

Or in code:

```c#
services.AddCKEditor(Configuration, options =>
{
    options.EditorClassName = "ClassicEditor";
});
```

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

and no `appsettings.json` config

Also to `App.razor` styles

```html

<link href="_content/Sitko.Blazor.CKEditor.Bundle/ckeditor.css" rel="stylesheet"/>
```

or

```html

<link href="_content/Sitko.Blazor.CKEditor.Bundle/ckeditor.dark.css" rel="stylesheet"/>
```

and scripts

```html

<script src="_content/Sitko.Blazor.CKEditor.Bundle/ckeditor.js"></script>
```

or

```html

<script src="_content/Sitko.Blazor.CKEditor.Bundle/ckeditor.dark.js"></script>
```
