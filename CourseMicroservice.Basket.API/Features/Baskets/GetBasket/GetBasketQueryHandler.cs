using CourseMicroservice.Basket.API.Constants;
using CourseMicroservice.Basket.API.Dto;
using CourseMicroservice.Shared;
using CourseMicroservice.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CourseMicroservice.Basket.API.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;

            var cacheKey = string.Format(BasketConstants.BasketCacheKey, userId);
            var basketAsString = await distributedCache.GetStringAsync(cacheKey, token: cancellationToken);

            if (basketAsString is null) return ServiceResult<BasketDto>.Error("Basket not found.", System.Net.HttpStatusCode.NotFound);

            var basket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

            return ServiceResult<BasketDto>.SuccessAsOk(basket!);
        }
    }
}
