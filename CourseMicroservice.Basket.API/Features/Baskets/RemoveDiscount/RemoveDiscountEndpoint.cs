using CourseMicroservice.Shared.Extensions;
using MediatR;

namespace CourseMicroservice.Basket.API.Features.Baskets.RemoveDiscount
{
    public static class RemoveDiscountEndpoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/user/remove-discount", async (IMediator mediator) => (await mediator.Send(new RemoveDiscountCommand())).ToGenericResult())
                .WithName("RemoveDiscountCoupon");

            return group;
        }
    }
}
