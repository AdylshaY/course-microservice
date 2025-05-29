using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CourseMicroservice.Shared.Extensions
{
    public static class ApiVersionExtension
    {
        public static IServiceCollection AddApiVersionExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        public static ApiVersionSet AddVersionSetExtension(this WebApplication app)
        {
            var apiVersionSet = app.NewApiVersionSet().HasApiVersion(new ApiVersion(1, 0)).HasApiVersion(new ApiVersion(2, 0)).ReportApiVersions().Build();

            return apiVersionSet;
        }
    }
}
