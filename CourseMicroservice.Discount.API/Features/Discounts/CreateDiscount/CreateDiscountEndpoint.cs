using CourseMicroservice.Shared.Filters;

namespace CourseMicroservice.Discount.API.Features.Discounts.CreateDiscount
{
    public static class CreateDiscountEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateDiscountCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateDiscount")
                .AddEndpointFilter<ValidationFilter<CreateDiscountCommand>>();

            return group;
        }
    }
}
