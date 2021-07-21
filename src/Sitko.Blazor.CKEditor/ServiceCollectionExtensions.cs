using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sitko.Blazor.CKEditor
{
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
            Action<CKEditorOptions>? postConfigure = null)
        {
            return serviceCollection.AddCKEditor<CKEditorOptions>(
                configuration, postConfigure: postConfigure);
        }
    }

    public interface ICKEditorOptionsProvider
    {
        CKEditorOptions Options { get; }
    }

    public class CKEditorOptionsProvider<TOptions> : ICKEditorOptionsProvider where TOptions : CKEditorOptions
    {
        private readonly IOptionsMonitor<TOptions> _optionsMonitor;
        public CKEditorOptions Options => _optionsMonitor.CurrentValue;

        public CKEditorOptionsProvider(IOptionsMonitor<TOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }
    }


    public class CKEditorOptions
    {
        [Required] public virtual string ScriptPath { get; set; } = "";
        [Required] public virtual string EditorClassName { get; set; } = "";
        public virtual CKEditorConfig CKEditorConfig { get; set; }
    }

    public class CKEditorConfig
    {
        public CKEditorToolbar Toolbar { get; set; }
        public string Language { get; set; }
    }

    public class CKEditorToolbar
    {
        public List<string> Items { get; set; } = new();
    }
}
