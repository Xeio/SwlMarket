using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SwlMarket.Data;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;

namespace SwlMarket
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
            services.AddDbContext<MarketContext>(options =>
                options.UseMySql(ConvertConnectionStringToMySQL(Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb"))));

            services.AddMvc();

            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Prices}");
            });

            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var marketContext = serviceScope.ServiceProvider.GetService<MarketContext>();
            marketContext.Database.Migrate();
        }

        /// <summary>
        /// Azure uses standard "Data Source" connection strings, but the MySQL connector doesn't seem to work with them. Fix them up here
        /// </summary>
        private static string ConvertConnectionStringToMySQL(string connectionString)
        {
            var mysqlConnectionString = new StringBuilder();
            var valuePairs = connectionString.Split(";");
            foreach (var pair in valuePairs)
            {
                var keyValue = pair.Split("=");
                string key = keyValue[0];
                string value = keyValue[1];

                switch (key)
                {
                    case "User Id":
                        mysqlConnectionString.Append($"uid={value};");
                        break;
                    case "Password":
                        mysqlConnectionString.Append($"pwd={value};");
                        break;
                    case "Data Source":
                        var serverPort = value.Split(":");
                        mysqlConnectionString.Append($"server={serverPort[0]};");
                        if(serverPort.Length > 0)
                        {
                            mysqlConnectionString.Append($"port={serverPort[1]};");
                        }
                        break;
                    default:
                        mysqlConnectionString.Append($"{key}={value};");
                        break;
                }
            }

            return mysqlConnectionString.ToString();
        }
    }
}
