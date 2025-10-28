namespace CourseMicroservice.Web.Pages.Auth.SignUp
{
    using CourseMicroservice.Web.Options;
    using CourseMicroservice.Web.Services;
    using Duende.IdentityModel.Client;

    public record KeycloakErrorResponse(string ErrorMessage);

    public class SignUpService(IdentityOption identityOption, HttpClient client, ILogger<SignUpService> logger)
    {
        public async Task<ServiceResult> CreateAccount(SignUpViewModel model)
        {
            try
            {
                var token = await GetClientCredentialTokenAsAdmin();
                var address = $"{identityOption.BaseAddress}/admin/realms/courseTenant/users";

                client.SetBearerToken(token);
                var userCreateRequest = CreateUserCreateRequest(model);
                var response = await client.PostAsJsonAsync(address, userCreateRequest);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.InternalServerError)
                    {
                        var keycloakErrorResponse = await response.Content.ReadFromJsonAsync<KeycloakErrorResponse>();
                        return ServiceResult.Error(keycloakErrorResponse!.ErrorMessage);
                    }

                    var error = await response.Content.ReadAsStringAsync();
                    logger.LogError(error);

                    return ServiceResult.Error("System error occurred, please try again later.");
                }

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An exception occurred while creating account.");
                return ServiceResult.Error("System error occurred, please try again later.");
            }

        }

        private static UserCreateRequest CreateUserCreateRequest(SignUpViewModel model)
        {
            return new UserCreateRequest(
                model.Username,
                true,
                model.FirstName,
                model.LastName,
                model.Email,
                [new Credential("password", model.Password, "false")]
            );
        }

        private async Task<string> GetClientCredentialTokenAsAdmin()
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Address,
                Policy = { RequireHttps = false }
            };

            client.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest);

            if (discoveryResponse.IsError) throw new Exception($"Failed to retrieve discovery document: {discoveryResponse.Error}");


            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryResponse.TokenEndpoint,
                    ClientId = identityOption.Admin.ClientId,
                    ClientSecret = identityOption.Admin.ClientSecret,
                });

            if (tokenResponse.IsError || string.IsNullOrEmpty(tokenResponse.AccessToken)) throw new Exception($"Failed to retrieve token: {tokenResponse.Error}");

            return tokenResponse.AccessToken;
        }
    }
}
