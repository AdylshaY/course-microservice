using CourseMicroservice.Basket.API.Constants;
using CourseMicroservice.Basket.API.Dto;
using CourseMicroservice.Shared;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CourseMicroservice.Basket.API.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributedCache) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            // TODO: Change userId
            Guid userId = Guid.NewGuid();
            var cacheKey = string.Format(BasketConstants.BasketCacheKey, userId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            BasketDto? currentBasket;
            var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new BasketDto(userId, [newBasketItem]);
                await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);
            var existingBasketItem = currentBasket!.BasketItemList.FirstOrDefault(x => x.Id == request.CourseId);

            if (existingBasketItem is not null)
            {
                currentBasket.BasketItemList.Remove(existingBasketItem);
            }

            currentBasket.BasketItemList.Add(newBasketItem);

            await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }

        private async Task CreateCacheAsync(BasketDto basketDto, string cacheKey, CancellationToken cancellationToken)
        {

            var basketAsString = JsonSerializer.Serialize(basketDto);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);
        }
    }
}
