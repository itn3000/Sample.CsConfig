using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Sample.CsConfig;

namespace Sample.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var configRoot = new ConfigurationBuilder()
                    .AddCs("appsettings.csx")
                    .Build();
                // root configurations
                foreach(var config in configRoot.GetChildren())
                {
                    Console.WriteLine($"{config.Path}/{config.Key} = {config.Value}");
                }
                // subsection configurations
                var subSection = configRoot.GetSection("d");
                foreach(var config in subSection.GetChildren())
                {
                    Console.WriteLine($"{config.Path}/{config.Key} = {config.Value}");
                }
                Console.WriteLine("Hello World");
            }
            catch (Exception e)
            {
                Console.WriteLine($"exception:{e.Message},{e.StackTrace}");
            }
            Console.Read();
        }
    }
}
