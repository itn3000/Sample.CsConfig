using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.CsConfig;

namespace Sample.WAPIApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddCs("appsettings.csx"
                    )
                ;
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddOptions();
            services.Configure<MyConfiguration>(Configuration);
            services.AddTransient<string>(svc=>{
                return "";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var logger = loggerFactory.CreateLogger("Startup");
            // get configroot's children
            var configs = Configuration.GetChildren()
                .Select(config => new { key = config.Key, path = config.Path, val = config.Value, })
                ;
            foreach (var config in Configuration.GetChildren())
            {
                // output config information
                logger.LogInformation($"key={config.Key},value={config.Value},path={config.Path}"
                    );
            }
            logger.LogInformation("child section");
            // get subsection
            var children = Configuration.GetSection("e");
            foreach(var config in children.GetChildren())
            {
                logger.LogInformation("key={0},value={1},path={2}"
                    , config.Key
                    , config.Value
                    , config.Path);
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
