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
using supermarketapi.Controllers.Config;
using supermarketapi.Domain.Repositories;
using supermarketapi.Domain.Services;
using supermarketapi.Persistence.Contexts;
using supermarketapi.Persistence.Repositories;
using supermarketapi.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Stripe;

namespace supermarketapi
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

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDbContext<AppDbContext>(options =>
            {              
                //sql server
                //options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);

                //mysql. this one is good
                //options.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]);

                options.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]);
            });


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketProductRepository, BasketProductRepository>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, Services.ProductService>();
            services.AddScoped<IBasketProductService, BasketProductService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddHealthChecks();

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
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

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}