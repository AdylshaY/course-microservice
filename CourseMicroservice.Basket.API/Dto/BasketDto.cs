using System.Text.Json.Serialization;

namespace CourseMicroservice.Basket.API.Dto
{
    public record BasketDto
    {
        [JsonIgnore]
        public Guid UserId { get; init; }

        public List<BasketItemDto> BasketItemList { get; init; }

        public BasketDto(Guid userId, List<BasketItemDto> basketItemList)
        {
            UserId = userId;
            BasketItemList = basketItemList;
        }

        public BasketDto()
        {
            
        }
    };
}
