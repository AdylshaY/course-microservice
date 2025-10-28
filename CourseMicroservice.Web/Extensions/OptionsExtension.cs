namespace CourseMicroservice.Web.Extensions
{
    using CourseMicroservice.Web.Options;
    using Microsoft.Extensions.Options;

    public static class OptionsExtension
    {
        public static IServiceCollection AddOptionsExtension(this IServiceCollection services)
        {
            services.AddOptions<IdentityOption>().BindConfiguration(nameof(IdentityOption)).ValidateDataAnnotations().ValidateOnStart();

            services.AddSingleton<IdentityOption>(sp => sp.GetRequiredService<IOptions<IdentityOption>>().Value);

            return services;
        }
    }
}
