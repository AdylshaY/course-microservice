namespace CourseMicroservice.Order.Application.Contracts.RefitServices.PaymentService
{
    public record CreatePaymentResponse(Guid? PaymentId, bool Status, string? ErrorMessage);
}
