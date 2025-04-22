using System.Text;
using Authentication.Domain.Entities;
using Authentication.Domain.Repository;
using Authentication.Domain.Services;
using Authentication.Infrastructure.Repositories;
using Authentication.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<User>(sp =>
        {
            var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
            if (httpContext == null)
                throw new UnauthorizedAccessException("HttpContext is not available.");
            return new User(httpContext.User);
        });
        
        var jwtSettings = configuration.GetSection("JwtSettings");

        services.AddSingleton<IJwtProvider>(provider => new JwtProvider(
            jwtSettings["SecretKey"],
            jwtSettings["Issuer"],
            jwtSettings["Audience"],
            int.Parse(jwtSettings["ExpiryInMinutes"])
        ));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"], 
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero 
                };
            });

        services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();

        return services;
    }
}