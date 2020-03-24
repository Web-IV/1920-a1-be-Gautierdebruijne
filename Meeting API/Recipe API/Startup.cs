using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using MeetingAPI.Data;
using MeetingAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingAPI
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

                //d.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token", new SwaggerSecurityScheme 
                //{ 
                //    Type = SwaggerSecuritySchemeType.ApiKey, 
                //    Name = "Authorization", 
                //    In = SwaggerSecurityApiKeyLocation.Header, 
                //    Description = "Copy 'Bearer' + valid JWT token into field" 
                //}));

                //Doesn't find all classes with given packages

                d.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme 
                { 
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Copy 'Bearer' + valid JWT token into field"
                });
                
                d.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            services.AddAuthentication(x => { x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; x.SaveToken = true; x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true
                };
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
            app.UseAuthentication();
            app.UseCors("AllowAllOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            init.InitializeData();
        }
    }
}
