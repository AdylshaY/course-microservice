namespace CourseMicroservice.Payment.API.Features.Payments.GetStatus
{
    using CourseMicroservice.Shared.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public static class GetPaymentStatusQueryEndpoint
    {
        public static RouteGroupBuilder GetPaymentStatusGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/status/{orderCode}", async ([FromServices] IMediator mediator, string orderCode) =>
            {
                var result = await mediator.Send(new GetPaymentStatusRequest(orderCode));
                return result.ToGenericResult();
            })
            .WithName("GetPaymentStatus")
            .MapToApiVersion(1, 0)
            .Produces(StatusCodes.Status200OK)
            .RequireAuthorization("ClientCredential");

            return group;
        }
    }
}
