using System.Text.Json.Serialization;

namespace CourseMicroservice.Basket.API.Dto
{
    public record BasketDto
    {
        [JsonIgnore]
        public bool IsAppliedDiscountRate => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);

        public List<BasketItemDto> BasketItemList { get; init; }

        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        public decimal TotalPrice => BasketItemList.Sum(item => item.Price);

        public decimal? TotalPriceWithAppliedDiscount => !IsAppliedDiscountRate ? null : BasketItemList.Sum(item => item.PriceByAppliedDiscountRate);

        public BasketDto(List<BasketItemDto> basketItemList)
        {
            BasketItemList = basketItemList;
        }
    };
}
