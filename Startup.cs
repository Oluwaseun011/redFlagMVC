using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using redFlag.Auth;
using redFlag.Context;
using redFlag.Repositories.Implementations;
using redFlag.Repositories.Interfaces;
using redFlag.Services.Implementations;
using redFlag.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redFlag
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
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationContext>(opt => opt.UseMySQL(Configuration.GetConnectionString("ConnectionContext")));

            services.AddScoped<IAgencyRepository, AgencyRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IInitiatorRepository, InitiatorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IAgencyService, AgencyService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IInitiatorService, InitiatorService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStaffService, StaffService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.LoginPath = "/User/Login";
                    config.Cookie.Name = "redFlag";
                    config.LogoutPath = "/User/Logout";
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(240);
                    config.AccessDeniedPath = "/User/Login";
                });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
