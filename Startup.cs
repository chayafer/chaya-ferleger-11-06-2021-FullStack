using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.AccWeatherRepository;
using Weather.BL;
using Weather.Contract;
using Weather.Dal;
using Weather.Middleware.ListsApi.Middleware;

namespace Weather
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
            services.AddDbContext<AccWeatherContext>(options => options.UseSqlServer(Configuration.GetConnectionString("accWeatherDb")));
            services.AddControllers();
            services.AddScoped<IFavorite, Favorite>();
            services.AddScoped<ICity, BL.City>();
            services.AddScoped<IWeather, BL.Weather>();
            services.AddScoped<IAccWeather, AccWeather>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware(typeof(ErrorHandler));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
