using LinkShortner.Context;
using LinkShortner.Services;
using Microsoft.EntityFrameworkCore;

namespace LinkShortner {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllersWithViews();

            //builder.Services.AddDbContext<LinkContext>(
            //    o => o.UseSqlServer(builder.Configuration.GetConnectionString("LinkDB")));

			var connectionString = builder.Configuration.GetConnectionString("LinkDB")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			builder.Services.ConfigureIdentity(connectionString); // Extention

			builder.Services.AddSingleton(typeof(StringService));

            //var configuration = builder.Configuration;
            //builder.Services.AddAuthentication().AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = configuration["Authentication:Google:ClientId"]; // in user secrets
            //    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            //}).AddFacebook(options =>
            //{
            //    options.AppId = configuration["Authentication:Facebook:AppId"];
            //    options.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            //    options.AccessDeniedPath = "/AccessDeniedPathInfo";
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


			app.Run();
        }
    }
}