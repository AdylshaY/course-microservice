using CourseMicroservice.Catalog.API.Features.Courses.GetAllByUserId;

namespace CourseMicroservice.Catalog.API.Features.Courses.GetCourseListByUserId
{
    public static class GetAllCourseListByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllCourseListByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}", async (IMediator mediator, Guid userId) => (await mediator.Send(new GetAllCourseListByUserIdQuery(userId))).ToGenericResult())
                .WithName("GetAllCourseListByUserId")
                .MapToApiVersion(1, 0)
                .RequireAuthorization(policyNames: "InstructorPolicy");

            return group;
        }
    }
}
