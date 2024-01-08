﻿using Application.Common.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Configs;
public static class AuthorizationConfig
{
    public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtApplicationConfig = configuration.GetSection("JwtApplicationConfig").Get<JwtApplicationConfig>()
            ?? throw new ArgumentNullException("Não foi possível carregar as configs do jwt da aplicação");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("JwtApplication", o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtApplicationConfig.Issuer,
                    ValidAudience = jwtApplicationConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtApplicationConfig.Key)),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                };
            });

        services.AddAuthorization(options =>
        {
            var jwtApplication = new AuthorizationPolicyBuilder("JwtApplication");
            options.AddPolicy("JwtApplication", jwtApplication
                .RequireAuthenticatedUser()
                .Build());

        });

        return services;
    }
}

