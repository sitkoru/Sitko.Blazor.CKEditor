namespace Sitko.Blazor.CKEditor;

using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScriptInjector;

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

        serviceCollection.AddScoped<ICKEditorLoader, CKEditorLoader>();
        serviceCollection.AddTransient<ICKEditorOptionsProvider, CKEditorOptionsProvider<TOptions>>();
        serviceCollection.AddScriptInjector();
        builder.ValidateDataAnnotations();

        return serviceCollection;
    }

    public static IServiceCollection AddCKEditor(this IServiceCollection serviceCollection,
        IConfiguration configuration,
        Action<CKEditorOptions>? postConfigure = null) =>
        serviceCollection.AddCKEditor<CKEditorOptions>(
            configuration, postConfigure: postConfigure);
}
