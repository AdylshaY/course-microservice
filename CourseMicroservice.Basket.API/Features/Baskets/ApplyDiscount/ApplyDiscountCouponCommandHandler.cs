using CourseMicroservice.Basket.API.Dto;
using CourseMicroservice.Shared;
using MediatR;
using System.Text.Json;

namespace CourseMicroservice.Basket.API.Features.Baskets.ApplyDiscount
{
    public class ApplyDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache(cancellationToken);

            if (string.IsNullOrEmpty(basketAsString)) return ServiceResult<BasketDto>.Error("Basket not found", System.Net.HttpStatusCode.NotFound);

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString)!;

            if (basket.BasketItemList.Count == 0)
            {
                return ServiceResult<BasketDto>.Error("Basket item not found", System.Net.HttpStatusCode.NotFound);
            }

            basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

            await basketService.CreateBasketCacheAsync(basket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
