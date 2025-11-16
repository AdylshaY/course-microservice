using CourseMicroservice.Web.Pages.Basket.Dtos;
using Refit;

namespace CourseMicroservice.Web.Services.Refit
{
    public interface IDiscountRefitService
    {
        [Get("/api/v1/discounts/{coupon}")]
        Task<ApiResponse<GetDiscountByCouponResponse>> GetDiscountByCoupon(string coupon);
    }
}
