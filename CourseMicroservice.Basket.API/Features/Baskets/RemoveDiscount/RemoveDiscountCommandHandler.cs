using CourseMicroservice.Basket.API.Constants;
using CourseMicroservice.Shared;
using CourseMicroservice.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CourseMicroservice.Basket.API.Features.Baskets.RemoveDiscount
{
    public class RemoveDiscountCommandHandler(IDistributedCache distributedCache, IIdentityService identityService, BasketService basketService) : IRequestHandler<RemoveDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache(cancellationToken);

            if (string.IsNullOrEmpty(basketAsString)) return ServiceResult.Error("Basket not found", System.Net.HttpStatusCode.NotFound);

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString)!;

            basket.RemoveDiscount();

            await basketService.CreateBasketCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
