using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sitko.Blazor.CKEditor.Bundle;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddCKEditorBundle(builder.Configuration);

await builder.Build().RunAsync();
