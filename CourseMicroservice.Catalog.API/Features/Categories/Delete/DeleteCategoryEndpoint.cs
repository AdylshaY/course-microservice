namespace CourseMicroservice.Catalog.API.Features.Categories.Delete
{
    public static class DeleteCategoryEndpoint
    {
        public static RouteGroupBuilder DeleteCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new DeleteCategoryCommand(id))).ToGenericResult())
                .WithName("DeleteCategory");

            return group;
        }
    }
}
