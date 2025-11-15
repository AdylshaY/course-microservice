using CourseMicroservice.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservice.Catalog.API.Features.Courses.Create
{
    public static class CreateCourseCommandEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromForm] CreateCourseCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateCourse")
                .Produces<CreateCourseResponse>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>()
                .DisableAntiforgery()
                .RequireAuthorization(policyNames: "InstructorPolicy");

            return group;
        }
    }
}
