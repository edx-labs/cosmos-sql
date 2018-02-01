using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateCatalog.Models;
using RealEstateCatalog.Services;
using RealEstateCatalog.Utilities;

namespace RealEstateCatalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddOptions();
            services.Configure<CosmosSettings>(Configuration.GetSection("Cosmos"));

            services.AddSingleton<IConfiguration>(Configuration);
            
            services.AddScoped<IHomesService, CosmosHomesService>();
            services.AddScoped<ISetupService, CosmosSetupService>();
            
            services.AddScoped<SetupActionFilter>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}