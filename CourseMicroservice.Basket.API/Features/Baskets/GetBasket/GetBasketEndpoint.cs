using CourseMicroservice.Shared.Extensions;
using MediatR;

namespace CourseMicroservice.Basket.API.Features.Baskets.GetBasket
{
    public static class GetBasketEndpoint
    {
        public static RouteGroupBuilder GetBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async (IMediator mediator) => (await mediator.Send(new GetBasketQuery())).ToGenericResult())
                .WithName("GetBasket");

            return group;
        }
    }
}
