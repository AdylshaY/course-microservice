using Asp.Versioning.Builder;
using CourseMicroservice.Basket.API.Features.Baskets.AddBasketItem;
using CourseMicroservice.Basket.API.Features.Baskets.ApplyDiscount;
using CourseMicroservice.Basket.API.Features.Baskets.DeleteBasketItem;
using CourseMicroservice.Basket.API.Features.Baskets.GetBasket;
using CourseMicroservice.Basket.API.Features.Baskets.RemoveDiscount;

namespace CourseMicroservice.Basket.API.Features.Baskets
{
    public static class BasketEndpointExtension
    {
        public static void AddBasketGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets")
                .WithTags("Basket")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketGroupItemEndpoint()
                .DeleteBasketGroupItemEndpoint()
                .GetBasketGroupItemEndpoint()
                .ApplyDiscountCouponGroupItemEndpoint()
                .RemoveDiscountCouponGroupItemEndpoint();
        }
    }
}
