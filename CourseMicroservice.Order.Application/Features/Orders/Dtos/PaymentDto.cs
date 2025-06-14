namespace CourseMicroservice.Order.Application.Features.Orders.Dtos;

public record PaymentDto(string CardNumber, string CardHolderName, string Expiration, string Cvc, decimal Amount);
