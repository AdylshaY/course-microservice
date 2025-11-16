namespace CourseMicroservice.Web.Pages.Basket.ViewModels
{
    public record BasketViewModelItem(
        Guid Id,
        string? PictureUrl,
        string Name,
        decimal Price,
        decimal? PriceWithDiscountRate);
}
