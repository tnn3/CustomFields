using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace CustomFields
{
    public class StaticFileConfiguration : IPostConfigureOptions<StaticFileOptions>
    {
        public StaticFileConfiguration(IHostingEnvironment environment)
        {
            Environment = environment;
        }
        public IHostingEnvironment Environment { get; }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && Environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? Environment.WebRootFileProvider;

            var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "wwwroot");
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }
}
