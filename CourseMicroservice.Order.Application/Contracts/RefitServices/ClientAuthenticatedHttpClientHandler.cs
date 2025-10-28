namespace CourseMicroservice.Order.Application.Contracts.RefitServices
{
    using CourseMicroservice.Shared.Options;
    using Duende.IdentityModel.Client;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading;
    using System.Threading.Tasks;

    public class ClientAuthenticatedHttpClientHandler(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization is not null) return await base.SendAsync(request, cancellationToken);

            using var scope = serviceProvider.CreateScope();
            var identityOptions = scope.ServiceProvider.GetRequiredService<IdentityOption>();
            var clientSecretOptions = scope.ServiceProvider.GetRequiredService<ClientSecretOptions>();

            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOptions.Address,
                Policy = { RequireHttps = false }
            };

            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(identityOptions.Address);
            var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest);

            if (discoveryResponse.IsError)
            {
                throw new Exception($"Failed to retrieve discovery document: {discoveryResponse.Error}");
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryResponse.TokenEndpoint,
                    ClientId = clientSecretOptions.ClientId,
                    ClientSecret = clientSecretOptions.ClientSecret,
                }, cancellationToken);

            if (tokenResponse.IsError || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                throw new Exception($"Failed to retrieve token: {tokenResponse.Error}");
            }

            request.SetBearerToken(tokenResponse.AccessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
