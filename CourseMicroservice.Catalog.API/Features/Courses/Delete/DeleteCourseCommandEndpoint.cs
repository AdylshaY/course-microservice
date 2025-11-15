namespace CourseMicroservice.Catalog.API.Features.Courses.Delete
{
    public static class DeleteCourseCommandEndpoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) => (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult())
                .WithName("DeleteCourse")
                .RequireAuthorization(policyNames: "InstructorPolicy");

            return group;
        }
    }
}
