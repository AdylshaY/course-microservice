using CourseMicroservice.Order.Application.Features.Orders.Create;
using CourseMicroservice.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservice.Order.API.Endpoints.Orders;

public static class CreateOrderEndpoint
{
    public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("CreateOrder")
            .MapToApiVersion(1, 0);

        return group;
    }
}
