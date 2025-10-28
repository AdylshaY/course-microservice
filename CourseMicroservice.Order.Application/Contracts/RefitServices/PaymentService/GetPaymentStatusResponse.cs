namespace CourseMicroservice.Order.Application.Contracts.RefitServices.PaymentService
{
    public record GetPaymentStatusResponse(Guid? PaymentId, bool IsPaid);
}
