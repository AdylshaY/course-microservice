using CourseMicroservice.Web.Services;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net;

namespace CourseMicroservice.Web.DelegateHandlers
{
    public class AuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor, TokenService tokenService) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (httpContextAccessor.HttpContext is null) return await base.SendAsync(request, cancellationToken);

            var user = httpContextAccessor.HttpContext.User;

            if (user is null || !user.Identity!.IsAuthenticated) return await base.SendAsync(request, cancellationToken);

            var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            if (string.IsNullOrEmpty(accessToken)) throw new UnauthorizedAccessException("No access token found.");

            request.SetBearerToken(accessToken);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode != HttpStatusCode.Unauthorized) return await base.SendAsync(request, cancellationToken);

            var refreshToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            if (string.IsNullOrEmpty(refreshToken)) throw new UnauthorizedAccessException("No refresh token found.");

            var tokenResponse = await tokenService.GetTokensByRefreshToken(refreshToken);

            if (tokenResponse.IsError || string.IsNullOrEmpty(tokenResponse.AccessToken)) throw new UnauthorizedAccessException("Failed to refresh access token.");

            // TODO: Update the authentication cookies with the new tokens here if necessary.

            request.SetBearerToken(tokenResponse.AccessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
