namespace CourseMicroservice.Web.Pages.Basket.Dtos
{
    public record BasketResponse(
        float? DiscountRate,
        string? Coupon,
        decimal TotalPrice,
        decimal? TotalPriceWithAppliedDiscount,
        List<BasketItemDto> BasketItemList
    );
}
