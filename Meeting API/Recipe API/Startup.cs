using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recipe_API.Data;
using Recipe_API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Recipe_API
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
            services.AddControllers();
            services.AddSwaggerDocument();

            services.AddDbContext<MeetingContext>(o => o.UseSqlServer(Configuration.GetConnectionString("MeetingContext")));

            services.AddScoped<MeetingDataInitializer>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();

            services.AddOpenApiDocument(d =>
            {
                d.DocumentName = "apidocs";
                d.Title = "Meeting API";
                d.Version = "v0.1";
                d.Description = "Documentation for the Meeting API, created by Gautier de Bruijne";
            });

            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MeetingDataInitializer init)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("AllowAllOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            init.InitializeData();
        }
    }
}
