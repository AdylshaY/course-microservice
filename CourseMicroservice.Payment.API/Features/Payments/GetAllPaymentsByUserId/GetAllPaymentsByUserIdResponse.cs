using CourseMicroservice.Payment.API.Repositories;

namespace CourseMicroservice.Payment.API.Features.Payments.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdResponse(Guid Id, string OrderCode, string Amount, DateTime Created, PaymentStatus Status);
}
