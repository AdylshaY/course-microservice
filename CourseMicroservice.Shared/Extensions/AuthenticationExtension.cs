using CourseMicroservice.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace CourseMicroservice.Shared.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddAuthenticationAndAuthorizationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var identityOptions = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();

            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = identityOptions!.Address;
                options.Audience = identityOptions.Audience;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            }).AddJwtBearer("ClientCredentialSchema", options =>
            {
                options.Authority = identityOptions!.Address;
                options.Audience = identityOptions.Audience;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                };
            });


            services.AddAuthorizationBuilder()
                .AddPolicy("ClientCredential", policy =>
                {
                    policy.AddAuthenticationSchemes("ClientCredentialSchema");
                    policy.RequireAuthenticatedUser();
                })
                .AddPolicy("Password", policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Email);
                });

            return services;
        }
    }
}
