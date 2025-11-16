namespace CourseMicroservice.Web.Pages.Order.Dtos
{
    public record PaymentDto(string CardNumber, string CardHolderName, string Expiration, string Cvc, decimal Amount);
}
