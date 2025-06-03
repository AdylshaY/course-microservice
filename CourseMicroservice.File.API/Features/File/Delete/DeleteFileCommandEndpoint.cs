using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservice.File.API.Features.File.Delete
{
    public static class DeleteFileCommandEndpoint
    {
        public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/", async ([FromBody] DeleteFileCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
                .WithName("delete");

            return group;
        }
    }
}
