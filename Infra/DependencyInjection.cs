using API.Services;
using Application.Common.Interfaces;
using Infra.Persistence;
using Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;
public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IUserService, UserService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordService, PasswordService>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("PostgresSql"),
            p =>
            {

                p.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                p.EnableRetryOnFailure(maxRetryCount: 4);
            });
        });

        return services;
    }
}

