using System.Text;
using AZTDinner.Application.Common.Interfaces;
using AZTDinner.Application.Common.Interfaces.Authentication;
using AZTDinner.Application.Common.Interfaces.Persistance;
using AZTDinner.Application.Common.Interfaces.Services;
using AZTDinner.Application.Serviecs.Authentication;
using AZTDinner.Infrastructure.Authentication;
using AZTDinner.Infrastructure.Persistence;
using AZTDinner.Infrastructure.Persistence.Inteceptors;
using AZTDinner.Infrastructure.Persistence.Repositories;
using AZTDinner.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AZTDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AppInfrastructure(this IServiceCollection services,
    ConfigurationManager configuration)
    {
        services.AddAuth(configuration)
                .AddPersistence();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<AZTDinnerDbContext>(option => option.UseSqlServer("Server=AZT-DELL;Database=AZTDinner;User Id=sa;Password=mm0ne20!5;TrustServerCertificate=true"));
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services,
   ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SessionName, jwtSettings);

        // services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SessionName));
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret))

        });
        return services;
    }
}