using CourseMicroservice.Basket.API.Constants;
using CourseMicroservice.Basket.API.Dto;
using CourseMicroservice.Shared;
using CourseMicroservice.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CourseMicroservice.Basket.API.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid userId = identityService.GetUserId;

            var cacheKey = string.Format(BasketConstants.BasketCacheKey, userId);
            var basketAsString = await distributedCache.GetStringAsync(cacheKey, token: cancellationToken);

            if (string.IsNullOrEmpty(basketAsString)) return ServiceResult.Error("Basket not found.", System.Net.HttpStatusCode.NotFound);

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var basketItemToDelete = currentBasket!.BasketItemList.FirstOrDefault(x => x.Id == request.CourseId);

            if (basketItemToDelete is null) return ServiceResult.Error("Basket item not found", System.Net.HttpStatusCode.NotFound);

            currentBasket.BasketItemList.Remove(basketItemToDelete);

            basketAsString = JsonSerializer.Serialize(currentBasket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, token: cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
