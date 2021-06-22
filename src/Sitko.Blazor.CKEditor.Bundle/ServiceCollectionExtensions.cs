using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sitko.Blazor.CKEditor.Bundle
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCKEditorBundle(this IServiceCollection serviceCollection,
            IConfiguration configuration,
            CKEditorTheme theme = CKEditorTheme.Light)
        {
            serviceCollection.AddCKEditor(configuration, options =>
            {
                var basePath = $"/_content/{typeof(CKEditorTheme).Assembly.GetName().Name}";
                options.ScriptPath = theme switch
                {
                    CKEditorTheme.Light => $"{basePath}/ckeditor.js",
                    CKEditorTheme.Dark => $"{basePath}/ckeditor.dark.js",
                    _ => throw new ArgumentOutOfRangeException()
                };
                options.EditorClassName = "BlazorEditor";
            });
            return serviceCollection;
        }
    }

    public enum CKEditorTheme
    {
        Light,
        Dark
    }
}
