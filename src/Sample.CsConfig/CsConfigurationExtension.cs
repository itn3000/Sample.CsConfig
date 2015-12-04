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
            IEnumerable<string> additionalFiles = null)
        {
            var provider = new CsConfigurationProvider(filePath, fileEncoding, additionalFiles);
            return builder.Add(provider);
        }
    }
}