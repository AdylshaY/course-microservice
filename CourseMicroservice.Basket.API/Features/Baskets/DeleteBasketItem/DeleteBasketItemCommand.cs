using CourseMicroservice.Shared;

namespace CourseMicroservice.Basket.API.Features.Baskets.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid CourseId) : IRequestByServiceResult;
}
