namespace CourseMicroservice.Order.Application.Contracts.RefitServices
{
    using CourseMicroservice.Order.Application.Contracts.RefitServices.PaymentService;
    using CourseMicroservice.Shared.Options;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Refit;

    public static class RefitConfiguration
    {
        public static IServiceCollection AddRefitConfigurationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthenticatedHttpClientHandler>();
            services.AddScoped<ClientAuthenticatedHttpClientHandler>();

            services.AddOptions<IdentityOption>().BindConfiguration(nameof(IdentityOption)).ValidateDataAnnotations().ValidateOnStart();
            services.AddSingleton<IdentityOption>(sp => sp.GetRequiredService<IOptions<IdentityOption>>().Value);

            services.AddOptions<ClientSecretOptions>().BindConfiguration(nameof(ClientSecretOptions)).ValidateDataAnnotations().ValidateOnStart();
            services.AddSingleton<ClientSecretOptions>(sp => sp.GetRequiredService<IOptions<ClientSecretOptions>>().Value);

            services.AddRefitClient<IPaymentService>().ConfigureHttpClient(cfg =>
            {
                var addressUrlOptions = configuration.GetSection(nameof(AddressUrlOptions)).Get<AddressUrlOptions>();
                cfg.BaseAddress = new Uri(addressUrlOptions!.PaymentUrl);
            }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>().AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();

            return services;
        }
    }
}
