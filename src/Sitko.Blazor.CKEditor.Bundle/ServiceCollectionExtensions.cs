using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sitko.Blazor.CKEditor.Bundle
{
    using JetBrains.Annotations;

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
}
