namespace CourseMicroservice.Order.Application.Contracts.RefitServices.PaymentService
{
    public record CreatePaymentRequest(string OrderCode, string CardNumber, string CardHolderName, string CardExpirationDate, string CardSecurityNumber, decimal Amount);
}
