namespace CourseMicroservice.File.API.Features.File.Upload
{
    public static class UploadFileCommandEndpoint
    {
        public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IFormFile file, IMediator mediator) => (await mediator.Send(new UploadFileCommand(file))).ToGenericResult())
                .WithName("upload")
                .DisableAntiforgery();

            return group;
        }
    }
}
