using AutoMapper;
using CourseMicroservice.Basket.API.Dto;
using CourseMicroservice.Shared;
using MediatR;
using System.Text.Json;

namespace CourseMicroservice.Basket.API.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(IMapper mapper, BasketService basketService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basketAsString = await basketService.GetBasketFromCache(cancellationToken);

            if (basketAsString is null) return ServiceResult<BasketDto>.Error("Basket not found.", System.Net.HttpStatusCode.NotFound);

            var basket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

            var basketDto = mapper.Map<BasketDto>(basket);

            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }
    }
}
