using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/* nuget */
using Microsoft.Extensions.Options;
using DevExpress.Blazor.DocumentMetadata;
using Demo.Blazor;

using MatBlazor;


using ShaipAgency.Areas.Identity;
using ShaipAgency.Model;

/* Shaip Service */
using ShaipAgency.Data;
using ShaipAgency.Data.Test;
using ShaipAgency.Data.Standards;
using ShaipAgency.Data.Deposits;



namespace ShaipAgency
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration[Configuration["DbMode"]]), ServiceLifetime.Transient);


            //services.AddDbContext<TestDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("LocalDbConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
            services.AddSingleton<WeatherForecastService>();

            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<ITestModelService, TestModelServiceUsingEF>();
            services.AddTransient<TestModelServiceDirectAccessDb>();

            /* 기준정보 입출력을 위한 서비스 */
            services.AddTransient<RequestCodeService>();
            services.AddTransient<RequestStatusCodeService>();
            services.AddTransient<RequestStatusRouteInfoService>();
            services.AddTransient<EventCodeService>();


            /* 예치금 관련 서비스 */
            services.AddTransient<DepositService>();

            services.AddDocumentMetadata((serviceProvider, registrator) =>
            {
                DemoConfiguration config = serviceProvider.GetService<IOptions<DemoConfiguration>>().Value;
                config.RegisterPagesMetadata(registrator);
            });

            services.AddDevExpressBlazor();
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 3000;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
