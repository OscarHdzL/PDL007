using Joonasw.AspNetCore.SecurityHeaders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Api_STEAR
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
            #region Configuracion IIS
            services.Configure<IISOptions>(o =>
            {
                o.ForwardClientCertificate = false;
            });
            #endregion

            #region Autentificaci�n JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(Configuration["JWT:ClaveSecreta"]))
                 };
             });
            #endregion

            #region Cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                builder.WithOrigins()
                         .AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .SetIsOriginAllowed(_ => true));
            });

            #endregion
            #region Swaggger
            services.AddSwaggerGen();
            #endregion
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Registros Asociaci�n Religiosa", Version = "v1" });
            });
          

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts(new HstsOptions
                {
                    Preload = false,
                    IncludeSubDomains = false,
                    Duration = new System.TimeSpan(365, 0, 0, 0)
                });

            }
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Registros Asociaci�n Religiosa v1"));

            app.UseMiddleware<SecurityHeadersMiddleware>();

            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("CorsPolicy");
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                /*endpoints.MapAreaControllerRoute(
                       "Operaciones",
                       "Operaciones",
                       "Operaciones/{controller=Home}/{action=Index}/{id?}");*/

                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            
                endpoints.MapSwagger("swagger/{documentName}/swagger.json");
         
            });
      
        }
    }
}
