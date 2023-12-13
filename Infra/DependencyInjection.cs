using Application.Common.Interfaces;
using Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infra.Services;

namespace Infra;
public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("connectionString"),
            p =>
            {

                p.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                p.EnableRetryOnFailure(maxRetryCount: 4);
            });
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordService, PasswordService>();

        return services;
    }
}

