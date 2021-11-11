using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movie.Api.Core.Implementation;
using Movie.Api.Core.Interface;
using Movie.Api.AutoMapper;
using Infrastructure.Provider.Interface;
using Infrastructure.Provider.Implementation;
using CinemaProxy;
using Microsoft.OpenApi.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Movie.Api
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
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            services.AddControllers();
            services.AddHttpClient();

            //Dependency Injection
            services.AddTransient<ICacheProvider, CacheProvider>();
            services.AddTransient<IMovieService, MovieService>();

            //services.AddHttpClient<Func<CinemaClient>>(client =>
            //{
            //    client.BaseAddress = new Uri();

            //});

            services.AddHttpClient<ICinemaClient, CinemaClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["ExternalService:CinemaService"]);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Movie API",
                    Description = "A Movie Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Developer",
                        Email = string.Empty,
                        Url = new Uri("https://example.com/developer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

          
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API V1");
                c.RoutePrefix = "swagger";
            });


            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
            app.UseForwardedHeaders();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{MovieController}/{action=Index}/{id?}");
            });
            app.UseMiddleware(typeof(SecurityResponseHeaderMiddleware));
        }
    }
}
