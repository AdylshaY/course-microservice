using CourseMicroservice.Shared.Extensions;
using CourseMicroservice.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservice.Payment.API.Features.Payments.Create
{
    public static class CreatePaymentCommandEndpoint
    {
        public static RouteGroupBuilder CreatePaymentGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] CreatePaymentCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("create")
                .AddEndpointFilter<ValidationFilter<CreatePaymentCommand>>()
                .MapToApiVersion(1, 0)
                .RequireAuthorization("Password");

            return group;
        }
    }
}
