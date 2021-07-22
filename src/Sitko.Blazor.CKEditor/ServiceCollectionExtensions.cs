using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sitko.Blazor.CKEditor
{
    using JetBrains.Annotations;

    [PublicAPI]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCKEditor<TOptions>(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            string optionsKey = "CKEditor",
            Action<TOptions>? postConfigure = null) where TOptions : CKEditorOptions
        {
            var builder = serviceCollection.AddOptions<TOptions>().Bind(configuration.GetSection(optionsKey));
            if (postConfigure is not null)
            {
                builder.PostConfigure(postConfigure);
            }

            serviceCollection.AddTransient<ICKEditorOptionsProvider, CKEditorOptionsProvider<TOptions>>();

            builder.ValidateDataAnnotations();

            return serviceCollection;
        }

        public static IServiceCollection AddCKEditor(this IServiceCollection serviceCollection,
            IConfiguration configuration,
            Action<CKEditorOptions>? postConfigure = null) =>
            serviceCollection.AddCKEditor<CKEditorOptions>(
                configuration, postConfigure: postConfigure);
    }
}
