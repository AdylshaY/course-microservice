namespace CourseMicroservice.Catalog.API.Features.Courses.GetAll
{
    public static class GetAllCourseEndpoint
    {
        public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllCourseQuery())).ToGenericResult())
                .WithName("GetAllCourse");

            return group;
        }
    }
}
