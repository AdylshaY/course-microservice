namespace CourseMicroservice.Web.Pages.Basket.ViewModels
{
    public record BasketItemViewModel(
        Guid Id,
        string Name,
        string ImageUrl,
        decimal Price,
        decimal? PriceByApplyDiscountRate);
}
