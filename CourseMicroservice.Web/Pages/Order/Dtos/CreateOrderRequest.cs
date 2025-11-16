namespace CourseMicroservice.Web.Pages.Order.Dtos
{
    public record CreateOrderRequest(
        float? DiscountRate,
        AddressDto Address,
        PaymentDto Payment,
        List<OrderItemDto> Items);
}
