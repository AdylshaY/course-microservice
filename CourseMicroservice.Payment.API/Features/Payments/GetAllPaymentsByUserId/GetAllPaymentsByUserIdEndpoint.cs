using CourseMicroservice.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservice.Payment.API.Features.Payments.GetAllPaymentsByUserId
{
    public static class GetAllPaymentsByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllPaymentByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllPaymentsByUserIdQuery())).ToGenericResult())
                .WithName("getAllPaymentsByUserId")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
