using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Introduction.IService;
using Introduction.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Introduction
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddApiVersioning(v =>
                {
                    v.AssumeDefaultVersionWhenUnspecified = true;
                    v.DefaultApiVersion = new ApiVersion(1, 0);
                    v.ReportApiVersions = true;
                    v.ApiVersionReader = ApiVersionReader.Combine(
                        new HeaderApiVersionReader("X-Version"),
                        new QueryStringApiVersionReader("ver", "version"),
                      new UrlSegmentApiVersionReader());

                }
            );
            services.AddSwaggerGen(setupAction =>
            {
                var xmlFile = "LatihanOpenAPI.xml";
                var xmlFullPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setupAction.IncludeXmlComments(xmlFullPath);

                setupAction.SwaggerDoc("LatihanOpenAPICategory",
                    new OpenApiInfo()
                    {
                        Title = "Latihan Open API-Category",
                        Version = "1",
                        Description = "Latihan membuat Web API dengan menggunakan.Net Core - Category",

                        Contact = new OpenApiContact()
                        {
                            Email = "Junindar@gmail.com",
                            Name = "Junindar",
                            Url = new Uri("http://junindar.blogspot.com")

                        }
                    });

                setupAction.SwaggerDoc("LatihanOpenAPIBook",
                    new OpenApiInfo()
                    {
                        Title = "Latihan Open API-Book",
                        Version = "1",
                        Description = "Latihan membuat Web API dengan menggunakan.Net Core - Book",

                        Contact = new OpenApiContact()
                        {
                            Email = "Junindar@gmail.com",
                            Name = "Junindar",
                            Url = new Uri("http://junindar.blogspot.com")

                        }
                    });

            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<PustakaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("pustakaConnection")));
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
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
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Error something went wrong. Please try again later.");
                    });
                });
            }
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/Swagger/LatihanOpenAPICategory/swagger.json", "Latihan Open API-Category");
                setupAction.SwaggerEndpoint("/Swagger/LatihanOpenAPIBook/swagger.json", "Latihan Open API-Book");
                setupAction.RoutePrefix = "";
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
