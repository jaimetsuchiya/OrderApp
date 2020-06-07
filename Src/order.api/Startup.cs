using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using order.api.Infrastructure;
using order.api.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace order.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "CorsPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowAnyOrigin();
                });
            });

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("SWBRASIL"));

            services.AddHttpContextAccessor();
            services.AddSingleton<IConfiguration>(this.Configuration);
            var audiences = this.Configuration.GetSection("Authentication:Audiences").Get<string[]>();
            var key = Encoding.ASCII.GetBytes(this.Configuration.GetSection("Authentication:SecretKey").Value);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudiences = audiences
                };
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderSandwichRepository, OrderSandwichRepository>();
            services.AddScoped<IPriceRuleRepository, PriceRuleRepository>();
            services.AddScoped<ISandwichRepository, SandwichRepository>();
            services.AddMediatR(this.GetType().Assembly);
            services.AddScoped<IAppUIContext>(provider =>
                            new AppUIContext(provider.GetService<IHttpContextAccessor>()                            )
                        ); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("CorsPolicy");
            });
        }
    }
}
