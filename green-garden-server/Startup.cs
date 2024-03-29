using green_garden_server.Data;
using green_garden_server.Managers;
using green_garden_server.Managers.Interfaces;
using green_garden_server.Messages;
using green_garden_server.Repositories;
using green_garden_server.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace green_garden_server
{
    public class Startup
    {
        readonly string AllowAllOrgins = "_allowAllOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowAllOrgins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
            services.AddDbContext<GreenGardenContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GreenGardenConnectionString")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "green_garden_server", Version = "v1" });
            });

            services.AddScoped<ILookupManager, LookupManager>();
            services.AddScoped<IProcessMessages, MessageProcessor>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<ILookupRepository, LookupRepository>();
            services.AddScoped<ISensorRepository, SensorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "green_garden_server v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(AllowAllOrgins);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
