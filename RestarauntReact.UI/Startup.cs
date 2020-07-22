namespace RestarauntReact.UI
{
    #region << Using >>

    using System;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using RestarauntReact.Core;

    #endregion

    public class Startup
    {
        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                                  {
                                      options.RequireHttpsMetadata = false;
                                      options.SaveToken = true;
                                      options.TokenValidationParameters = new TokenValidationParameters
                                                                          {
                                                                                  ValidIssuer = "ValidIssuer",
                                                                                  ValidAudience = "ValidateAudience",
                                                                                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IssuerSigningSecretKey")),
                                                                                  ValidateLifetime = true,
                                                                                  ValidateIssuerSigningKey = true,
                                                                                  ClockSkew = TimeSpan.Zero
                                                                          };
                                  });

            services.AddControllersWithViews();

            NHibernateRepositories.InitSessionFactory(Configuration.GetConnectionString("DefaultConnection"));

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
                                       {
                                           configuration.RootPath = "ClientApp/build";
                                       });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllerRoute(
                                                              "default",
                                                              "{controller}/{action=Index}/{id?}");
                             });

            app.UseSpa(spa =>
                       {
                           spa.Options.SourcePath = "ClientApp";

                           if (env.IsDevelopment())
                               spa.UseReactDevelopmentServer("start");
                       });
        }
    }
}