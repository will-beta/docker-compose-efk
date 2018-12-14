using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FluentdDotNetCoreTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddLogging(b =>
            {
                b.AddConfiguration(Configuration.GetSection("Logging"));

                var config = new LoggerConfiguration()
                .ReadFrom.ConfigurationSection(Configuration.GetSection("Logging:Serilog"))
                //.WriteTo.Fluentd(new Serilog.Sinks.Fluentd.FluentdSinkOptions("42.159.89.9", 24224)
                //{
                //    LingerEnabled = false,
                //    NoDelay = true,
                //    Tag = "FluentdDotNetCoreTest"
                //})
                ;
                var logger = config.CreateLogger()
                ;
                b.AddSerilog(logger);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory)
        {
            //app.UseExceptionless("vYAJulY8WNmU9v72gFspZkzp3S04yRpQnGdwrEUy");
            //factory.AddExceptionless();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
