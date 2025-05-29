namespace CourseMicroservice.Basket.API.Data
{
    public class Basket
    {
        public Basket()
        {

        }

        public Basket(Guid userId, List<BasketItem> basketItemList)
        {
            UserId = userId;
            BasketItemList = basketItemList;
        }

        public Guid UserId { get; set; }
        public List<BasketItem> BasketItemList { get; set; } = [];
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        public decimal TotalPrice => BasketItemList.Sum(item => item.Price);

        public bool IsAppiedDiscountRate => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);

        public decimal? TotalPriceWithAppliedDiscount => !IsAppiedDiscountRate ? null : BasketItemList.Sum(item => item.PriceByAppliedDiscountRate);

        public void ApplyNewDiscount(string coupon, float discountRate)
        {
            Coupon = coupon;
            DiscountRate = discountRate;
            foreach (var item in BasketItemList)
            {
                item.PriceByAppliedDiscountRate = item.Price * (decimal)(1 - discountRate);
            }
        }

        public void ApplyAvailableDiscount()
        {
            foreach (var item in BasketItemList)
            {
                item.PriceByAppliedDiscountRate = item.Price * (decimal)(1 - DiscountRate!);
            }
        }

        public void RemoveDiscount()
        {
            Coupon = null;
            DiscountRate = null;
            foreach (var item in BasketItemList)
            {
                item.PriceByAppliedDiscountRate = null;
            }
        }
    }
}
