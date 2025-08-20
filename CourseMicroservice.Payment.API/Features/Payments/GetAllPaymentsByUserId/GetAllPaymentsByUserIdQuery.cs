using CourseMicroservice.Shared;

namespace CourseMicroservice.Payment.API.Features.Payments.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;
}
