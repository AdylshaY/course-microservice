using CourseMicroservice.Shared;
using CourseMicroservice.Shared.Services;
using MediatR;
using System.Text.Json;

namespace CourseMicroservice.Basket.API.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IIdentityService identityService, BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache(cancellationToken);

            Data.Basket? currentBasket;
            var newBasketItem = new Data.BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new Data.Basket(identityService.GetUserId, [newBasketItem]);
                await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }

            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);
            var existingBasketItem = currentBasket!.BasketItemList.FirstOrDefault(x => x.Id == request.CourseId);

            if (existingBasketItem is not null)
            {
                currentBasket.BasketItemList.Remove(existingBasketItem);
            }

            currentBasket.BasketItemList.Add(newBasketItem);
            currentBasket.ApplyAvailableDiscount();
            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
