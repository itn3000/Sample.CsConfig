using Microsoft.Extensions.Configuration;
using System.Text;
using System.Collections.Generic;

namespace Sample.CsConfig
{
    public static class CsConfigurationExtension
    {
        public static IConfigurationBuilder AddCs(
            this IConfigurationBuilder builder,
            string filePath,
            Encoding fileEncoding = null,
            IEnumerable<string> preloadFiles = null)
        {
            var provider = new CsConfigurationProvider(filePath, fileEncoding, preloadFiles);
            return builder.Add(provider);
        }
    }
}