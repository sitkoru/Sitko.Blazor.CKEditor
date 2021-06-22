using System;
using Microsoft.Extensions.DependencyInjection;

namespace Sitko.Blazor.CKEditor
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCKEditor(this IServiceCollection serviceCollection,
            Action<CKEditorOptions>? configure = null)
        {
            if (configure is not null)
            {
                serviceCollection.Configure(configure);
            }

            return serviceCollection;
        }
    }

    public class CKEditorOptions
    {
        public string? CustomScriptPath { get; set; }
        public string CKEditorClassName { get; set; } = "BlazorEditor";

        public CKEditorTheme Theme { get; set; } = CKEditorTheme.Light;

        public string GetScriptPath()
        {
            if (!string.IsNullOrEmpty(CustomScriptPath))
            {
                return CustomScriptPath;
            }

            var basePath = $"/_content/{typeof(CKEditor).Assembly.GetName().Name}";
            return Theme switch
            {
                CKEditorTheme.Light => $"{basePath}/ckeditor.js",
                CKEditorTheme.Dark => $"{basePath}/ckeditor.dark.js",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum CKEditorTheme
    {
        Light,
        Dark
    }
}
