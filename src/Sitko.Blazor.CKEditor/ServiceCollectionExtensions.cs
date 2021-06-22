using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sitko.Blazor.CKEditor
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCKEditor(this IServiceCollection serviceCollection,
            IConfiguration configuration,
            Action<CKEditorOptions>? postConfigure = null)
        {
            var builder = serviceCollection.AddOptions<CKEditorOptions>().Bind(configuration.GetSection("CKEditor"));
            if (postConfigure is not null)
            {
                builder.PostConfigure(postConfigure);
            }

            builder.ValidateDataAnnotations();

            return serviceCollection;
        }
    }

    public class CKEditorOptions
    {
        [Required] public string ScriptPath { get; set; } = "";
        [Required] public string EditorClassName { get; set; } = "";
    }
}
