using Application.Common.Configs;

namespace API.Configs;

public static class LoadConfigs
{
    public static IServiceCollection AddConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtApplicationConfig>(configuration.GetSection("JwtApplicationConfig"));

        services.Configure<JwtPasswordConfig>(configuration.GetSection("JwtPasswordConfig"));

        services.Configure<EmailConfig>(configuration.GetSection("EmailConfig"));

        services.Configure<FileConfig>(configuration.GetSection("FileConfig"));

        return services;
    }
}


