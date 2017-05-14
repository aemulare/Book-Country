using BookCountry.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NToastNotify;
using NToastNotify.Constants;

namespace BookCountry
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }



        public IConfigurationRoot Configuration { get; }



        public void ConfigureServices(IServiceCollection svc)
        {
            svc.AddSingleton(Configuration);
            svc.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            svc.AddTransient<IBooksRepository,BooksRepository>();
            svc.AddTransient<ILoansRepository,LoansRepository>();
            svc.AddTransient<IBorrowersRepository,BorrowersRepository>();
            svc.AddNToastNotify(new ToastOption
            {
                ProgressBar = false,
                PositionClass = ToastPositions.TopRight
            });
            svc.AddMvc();
            svc.AddMemoryCache();
            svc.AddSession();
        }



        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                LoginPath = new PathString("/Accounts/Login"),
                AutomaticAuthenticate = true
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Books}/{action=Tile}/{id?}");
            });
        }
    }
}
