namespace CourseMicroservice.Web.Pages.Basket.Dtos
{
    public record BasketItemDto(
        Guid Id,
        string Name,
        string ImageUrl,
        decimal Price,
        decimal? PriceByApplyDiscountRate);
}
