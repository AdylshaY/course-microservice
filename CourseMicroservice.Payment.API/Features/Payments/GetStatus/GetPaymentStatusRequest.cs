namespace CourseMicroservice.Payment.API.Features.Payments.GetStatus
{
    using CourseMicroservice.Shared;

    public record GetPaymentStatusRequest(string OrderCode) : IRequestByServiceResult<GetPaymentStatusResponse>;
}
