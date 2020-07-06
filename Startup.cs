using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Supermarket.API.Controllers.Config;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Persistence.Contexts;
using Supermarket.API.Persistence.Repositories;
using Supermarket.API.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace Supermarket.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddControllers();

            services.AddMvc().AddNewtonsoftJson();

            // Add Cors
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //services.AddSwaggerGen(cfg =>
            //{
            //    cfg.SwaggerDoc("v1", new Info
            //    {
            //        Title = "Supermarket API",
            //        Version = "v1.1",
            //        Description = "Simple RESTful API built with ASP.NET Core 2.2 to show how to create RESTful services using a decoupled, maintainable architecture.",
            //        Contact = new Contact
            //        {
            //            Name = "Evandro Gayer Gomes",
            //            Url = "https://github.com/evgomes",
            //        },
            //        License = new License
            //        {
            //            Name = "MIT",
            //        },
            //    });

            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    cfg.IncludeXmlComments(xmlPath);
            //});

            //services.AddMvc()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_3_1)
            //    .ConfigureApiBehaviorOptions(options =>
            //    {
            //        // Adds a custom error response factory when ModelState is invalid
            //        options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            //    });


            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseInMemoryDatabase("supermarket-api-in-memory");

                //sql server
                //options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);

                //mysql. this one is good
                //options.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]);

                //var host = Configuration["DBHOST"] ?? "localhost";

                //options.UseMySQL($"server={host};port=3306;database=db;uid=root;password=Passw0rd");

                options.UseMySQL($"server=databasedelivery.ci6cuzvco6cc.us-east-2.rds.amazonaws.com;port=3306;database=databasemaster;uid=admin;password=Gen21ratoy");

            });


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseSwagger();

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Supermarket API");
            //});

            app.UseCors("MyPolicy");

            //app.UseHttpsRedirection();


            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

        }
    }
}