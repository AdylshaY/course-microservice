using CourseMicroservice.Order.Application.Features.Orders.Dtos;

namespace CourseMicroservice.Order.Application.Features.Orders.GetOrderList
{
    public record GetOrderListQueryResponse(DateTime Created, decimal TotalPrice, List<OrderItemDto> OrderItemList);
}
