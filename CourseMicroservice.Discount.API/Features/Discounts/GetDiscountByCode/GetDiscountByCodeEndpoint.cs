namespace CourseMicroservice.Discount.API.Features.Discounts.GetDiscountByCode
{
    public static class GetDiscountByCodeEndpoint
    {
        public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{code:length(10)}", async (string code, IMediator mediator) => (await mediator.Send(new GetDiscountByCodeQuery(code))).ToGenericResult())
            .WithName("GetDiscountByCode");

            return group;
        }
    }
}
