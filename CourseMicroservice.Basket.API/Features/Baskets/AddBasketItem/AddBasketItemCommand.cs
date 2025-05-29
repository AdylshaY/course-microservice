using CourseMicroservice.Shared;

namespace CourseMicroservice.Basket.API.Features.Baskets.AddBasketItem
{
    public record AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string? ImageUrl) : IRequestByServiceResult;
}
