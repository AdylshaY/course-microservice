using CourseMicroservice.Basket.API.Dto;
using CourseMicroservice.Shared;

namespace CourseMicroservice.Basket.API.Features.Baskets.GetBasket
{
    public record GetBasketQuery : IRequestByServiceResult<BasketDto>;
}
