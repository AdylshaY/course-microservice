using CourseMicroservice.Shared.Filters;

namespace CourseMicroservice.Catalog.API.Features.Courses.Update
{
    public static class UpdateCourseCommandEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateCourseCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("UpdateCourse")
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>()
                .RequireAuthorization(policyNames: "InstructorPolicy");

            return group;
        }
    }
}
