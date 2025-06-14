namespace CourseMicroservice.Order.Application.Features.Orders.Dtos;

public record OrderItemDto(Guid ProductId, string ProductName, decimal UnitPrice);
