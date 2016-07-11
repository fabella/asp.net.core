using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using aspnetcoreapp.Services;

namespace aspnetcoreapp
{
    public class Startup 
    {
        public IConfiguration Configuration { get; set; }
        public Startup() 
        {
            Configuration = new ConfigurationBuilder()
                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provier => Configuration);
            services.AddSingleton<IGreeter, Greeter>();
        }
        public void Configure(
            IApplicationBuilder app,
            IGreeter greeter) 
        {
            app.UseFileServer();
            app.Run(async (context) => {
                var greeting = greeter.GetGreeting();
                await context.Response.WriteAsync(greeting);
            });
        }          
    }
}