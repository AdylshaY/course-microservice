using CourseMicroservice.Shared;
using MediatR;
using System.Text.Json;

namespace CourseMicroservice.Basket.API.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache(cancellationToken);

            if (string.IsNullOrEmpty(basketAsString)) return ServiceResult.Error("Basket not found.", System.Net.HttpStatusCode.NotFound);

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var basketItemToDelete = currentBasket!.BasketItemList.FirstOrDefault(x => x.Id == request.CourseId);

            if (basketItemToDelete is null) return ServiceResult.Error("Basket item not found", System.Net.HttpStatusCode.NotFound);

            currentBasket.BasketItemList.Remove(basketItemToDelete);

            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
