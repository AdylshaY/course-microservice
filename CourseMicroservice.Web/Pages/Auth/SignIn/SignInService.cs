using CourseMicroservice.Web.Options;
using CourseMicroservice.Web.Services;
using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CourseMicroservice.Web.Pages.Auth.SignIn
{
    public class SignInService(IHttpContextAccessor httpContextAccessor, TokenService tokenService, IdentityOption identityOption, HttpClient client, ILogger<SignInService> logger)
    {
        public async Task<ServiceResult> AuthenticateAsync(SignInViewModel signInViewModel)
        {
            try
            {
                var tokenResponse = await GetAccessToken(signInViewModel);

                if (tokenResponse.IsError || string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    logger.LogWarning("Token request failed: {Error}", tokenResponse.Error);
                    return ServiceResult.Error(tokenResponse.Error!, tokenResponse.ErrorDescription!);
                }

                var userClaims = tokenService.ExtractClaims(tokenResponse.AccessToken);
                var authenticationProperties = tokenService.CreateAuthenticationProperties(tokenResponse);

                var claimIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

                await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An exception occurred while signing in.");
                return ServiceResult.Error("An exception occurred while signing in, please try again later.");
            }
        }

        private async Task<TokenResponse> GetAccessToken(SignInViewModel signInViewModel)
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Address,
                Policy = { RequireHttps = false }
            };

            client.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest);

            if (discoveryResponse.IsError) throw new Exception($"Failed to retrieve discovery document: {discoveryResponse.Error}");

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                UserName = signInViewModel.Email,
                Password = signInViewModel.Password,
            });

            if (tokenResponse.IsError || string.IsNullOrEmpty(tokenResponse.AccessToken)) throw new Exception($"Failed to retrieve token: {tokenResponse.Error}");

            return tokenResponse;
        }
    }
}
