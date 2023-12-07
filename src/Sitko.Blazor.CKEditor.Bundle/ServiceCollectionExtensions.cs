namespace Sitko.Blazor.CKEditor.Bundle;

using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[PublicAPI]
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
