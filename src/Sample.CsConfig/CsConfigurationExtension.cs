using Microsoft.Extensions.Configuration;
using System.Text;

namespace Sample.CsConfig
{
    public static class CsConfigurationExtension
    {
        public static IConfigurationBuilder AddCs(
            this IConfigurationBuilder builder,
            string filePath,
            Encoding fileEncoding = null)
        {
            var provider = new CsConfigurationProvider(filePath,fileEncoding);
            return builder.Add(provider);
        }
    }
}