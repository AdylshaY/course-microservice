using FluentValidation;

namespace CourseMicroservice.Basket.API.Features.Baskets.ApplyDiscount
{
    public class ApplyDiscountCouponCommandValidator : AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponCommandValidator()
        {
            RuleFor(x => x.Coupon).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.DiscountRate).InclusiveBetween(0,1).WithMessage("{PropertyName} must be between {From} and {To}.");
        }
    }
}
