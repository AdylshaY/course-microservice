using Asp.Versioning.Builder;
using CourseMicroservice.Payment.API.Features.Payments.Create;
using CourseMicroservice.Payment.API.Features.Payments.GetAllPaymentsByUserId;

namespace CourseMicroservice.Payment.API.Features.Payments
{
    public static class PaymentEndpointExtension
    {
        public static void AddPaymentGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/payments")
                .WithTags("payment")
                .WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItemEndpoint()
                .GetAllPaymentByUserIdGroupItemEndpoint()
                .RequireAuthorization("Password");
        }
    }
}
