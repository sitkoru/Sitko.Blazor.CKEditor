# Sitko.Blazor.CKEditor

CKEditor component for Blazor Applications

# Installation

```
dotnet add package Sitko.Blazor.CKEditor
```

Register in DI and configure

```c#
services.AddCKEditor(options =>
{
    options.Theme = CKEditorTheme.Dark;
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

# Custom editor

This package includes basic ckeditor build with light and dark themes. But you can use your own ckeditor build. Just specify `CustomScriptPath` and `CKEditorClassName` while configuring options.

```c#
services.AddCKEditor(options =>
{
    options.CustomScriptPath = "https://cdn.ckeditor.com/ckeditor5/28.0.0/classic/ckeditor.js";
    options.CKEditorClassName = "ClassicEditor";
});
```


