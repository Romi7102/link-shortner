using LinkShortner.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


public static class IdentityConfiguration {
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services, string connectionString) {

        services.AddDbContext<LinkContext>(options =>
                options.UseSqlServer(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options =>
            options.SignIn.RequireConfirmedAccount = true
        )
            .AddEntityFrameworkStores<LinkContext>();

        services.Configure<IdentityOptions>(o => {
            o.Password.RequiredUniqueChars = 0;
            o.Password.RequireUppercase = false;
            o.Password.RequireLowercase = false;
            o.Password.RequiredLength = 4;
            o.Password.RequireNonAlphanumeric = false;

            // Default Lockout settings.
            o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            o.Lockout.MaxFailedAccessAttempts = 5;
            o.Lockout.AllowedForNewUsers = true;
        });

       

        return services;
    }
}