using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sitko.Blazor.CKEditor.Bundle
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCKEditorBundle(this IServiceCollection serviceCollection,
            IConfiguration configuration, Action<CKEditorBundleOptions>? postConfigure = null)
        {
            serviceCollection.AddCKEditor<CKEditorBundleOptions>(configuration,
                "CKEditorBundle", options =>
                {
                    postConfigure?.Invoke(options);
                });
            return serviceCollection;
        }
    }

    public class CKEditorBundleOptions : CKEditorOptions
    {
        public CKEditorTheme Theme { get; set; } = CKEditorTheme.Light;

        public static CKEditorConfig DefaultConfig => new()
        {
            Toolbar = new CKEditorToolbar
            {
                Items = new List<string>
                {
                    "heading",
                    "|",
                    "bold",
                    "italic",
                    "link",
                    "bulletedList",
                    "numberedList",
                    "undo",
                    "redo"
                }
            }
        };

        public override string ScriptPath => Theme switch
        {
            CKEditorTheme.Light => $"{basePath}/ckeditor.js",
            CKEditorTheme.Dark => $"{basePath}/ckeditor.dark.js",
            _ => throw new ArgumentOutOfRangeException()
        };

        public override string EditorClassName { get; set; } = "BlazorEditor";
        private string basePath => $"/_content/{typeof(CKEditorTheme).Assembly.GetName().Name}";
    }

    public enum CKEditorTheme
    {
        Light,
        Dark
    }
}
