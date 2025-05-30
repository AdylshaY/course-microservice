using CourseMicroservice.Shared;

namespace CourseMicroservice.Basket.API.Features.Baskets.ApplyDiscount
{
    public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate) : IRequestByServiceResult;

}
