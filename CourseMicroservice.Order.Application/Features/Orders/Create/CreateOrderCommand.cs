using CourseMicroservice.Order.Application.Features.Orders.Dtos;
using CourseMicroservice.Shared;

namespace CourseMicroservice.Order.Application.Features.Orders.Create;

public record CreateOrderCommand(float? DiscountRate, AddressDto Address, PaymentDto Payment, List<OrderItemDto> Items) : IRequestByServiceResult;
