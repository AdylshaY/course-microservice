namespace CourseMicroservice.Catalog.API.Features.Courses.GetById
{
    public static class GetByIdCourseEndpoint
    {
        public static RouteGroupBuilder GetByIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new GetByIdCourseQuery(id))).ToGenericResult())
                .WithName("GetByIdCourse");

            return group;
        }
    }
}
