using Asp.Versioning.Builder;
using CourseMicroservice.Shared.Filters;

namespace CourseMicroservice.Catalog.API.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateCategory")
                .MapToApiVersion(1,0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return group;
        }
    }
}
