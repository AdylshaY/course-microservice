using CourseMicroservice.Basket.API.Constants;
using CourseMicroservice.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CourseMicroservice.Basket.API.Features.Baskets
{
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache)
    {
        private string GetBasketCacheKey() => string.Format(BasketConstants.BasketCacheKey, identityService.UserId);
        private string GetBasketCacheKey(Guid UserId) => string.Format(BasketConstants.BasketCacheKey, UserId);

        public Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
        {
            return distributedCache.GetStringAsync(GetBasketCacheKey(), cancellationToken);
        }

        public async Task CreateBasketCacheAsync(Data.Basket basketDto, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basketDto);
            await distributedCache.SetStringAsync(GetBasketCacheKey(), basketAsString, token: cancellationToken);
        }

        public async Task DeleteBasket(Guid userId)
        {
            await distributedCache.RemoveAsync(GetBasketCacheKey(userId));
        }
    }
}
