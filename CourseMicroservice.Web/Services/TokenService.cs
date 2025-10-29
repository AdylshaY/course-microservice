﻿using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CourseMicroservice.Web.Services
{
    public class TokenService
    {
        public List<Claim> ExtractClaims(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            return jwtToken.Claims.ToList();
        }

        public AuthenticationProperties CreateAuthenticationProperties(TokenResponse tokenResponse)
        {
            var authenticationTokens = new List<AuthenticationToken>()
            {
                new() { Name = OpenIdConnectParameterNames.AccessToken, Value = tokenResponse.AccessToken! },
                new() { Name = OpenIdConnectParameterNames.RefreshToken, Value = tokenResponse.RefreshToken! },
                new() { Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).ToString("o") },
            };

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            authenticationProperties.StoreTokens(authenticationTokens);

            return authenticationProperties;
        }
    }
}
