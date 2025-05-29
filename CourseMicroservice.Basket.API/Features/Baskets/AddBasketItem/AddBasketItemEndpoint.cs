using CourseMicroservice.Shared.Extensions;
using CourseMicroservice.Shared.Filters;
using MediatR;

namespace CourseMicroservice.Basket.API.Features.Baskets.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("AddBasketItem")
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommand>>();

            return group;
        }
    }
}
