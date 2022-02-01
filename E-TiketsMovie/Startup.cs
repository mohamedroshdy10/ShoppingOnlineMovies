using E_TiketsMovie.Data;
using E_TiketsMovie.Models.Tables.Peopel;
using E_TiketsMovie.Reposteries;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_TiketsMovie
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
            services.AddRazorPages();
            services.AddDbContext<EcommercdbContext>(op =>
            {
                op.UseSqlServer(Configuration.GetConnectionString("DD"));
            });
            services.AddIdentity<AppliationUser, IdentityRole>()
               .AddEntityFrameworkStores<EcommercdbContext>()
               .AddDefaultTokenProviders();
            services.AddTransient(typeof(IActorRepostery), typeof(ActorResotery));
            services.AddTransient(typeof(ActorResotery));
            services.AddTransient(typeof(IProdusserRepsotery), typeof(ProusserRepsotery));
            services.AddTransient(typeof(ProusserRepsotery));
            services.AddTransient(typeof(ICinemaRepostery), typeof(CinemaRepostery));
            services.AddTransient(typeof(CinemaRepostery));
            services.AddTransient(typeof(IMovieRepsotery), typeof(MoiveRepostery));
            services.AddTransient(typeof(MoiveRepostery));
            services.AddTransient(typeof(IOrderRepostery), typeof(OrderRepostery));
            services.AddTransient(typeof(OrderRepostery));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sc => ShoppingCard.GetShoppingCart(sc));
            services.AddMemoryCache();
            services.AddSession();

            services.AddAuthentication(au =>
            {
                au.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
            services.AddControllers();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Movies}/{action=Index}/{id?}");
            });
             AppDbInteliser.SeedUsersAndRolesAsync(app).Wait();
        }
    }
}
