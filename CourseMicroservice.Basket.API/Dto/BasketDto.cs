namespace CourseMicroservice.Basket.API.Dto
{
    public record BasketDto(Guid UserId, List<BasketItemDto> BasketItemList);
}
