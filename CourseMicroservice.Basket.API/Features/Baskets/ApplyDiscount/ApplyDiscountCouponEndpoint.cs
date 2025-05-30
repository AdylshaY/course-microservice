using CourseMicroservice.Shared.Extensions;
using CourseMicroservice.Shared.Filters;
using MediatR;

namespace CourseMicroservice.Basket.API.Features.Baskets.ApplyDiscount
{
    public static class ApplyDiscountCouponEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/user/apply-discount", async (ApplyDiscountCouponCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("ApplyDiscountCoupon")
                .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();

            return group;
        }
    }
}
