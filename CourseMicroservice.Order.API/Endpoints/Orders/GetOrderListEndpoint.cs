using CourseMicroservice.Order.Application.Features.Orders.GetOrderList;
using CourseMicroservice.Shared.Extensions;
using MediatR;

namespace CourseMicroservice.Order.API.Endpoints.Orders
{
    public static class GetOrderListEndpoint
    {
        public static RouteGroupBuilder GetOrderListGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetOrderListQuery())).ToGenericResult())
            .WithName("GetOrderList");

            return group;
        }
    }
}
