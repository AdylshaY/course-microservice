namespace CourseMicroservice.Payment.API.Features.Payments.Create
{
    public record CreatePaymentResponse(Guid? PaymentId, bool Status, string? ErrorMessage);
}
