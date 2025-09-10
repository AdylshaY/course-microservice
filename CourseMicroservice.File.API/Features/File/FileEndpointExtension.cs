using CourseMicroservice.File.API.Features.File.Delete;
using CourseMicroservice.File.API.Features.File.Upload;

namespace CourseMicroservice.File.API.Features.File
{
    public static class FileEndpointExtension
    {
        public static void AddFileGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files")
                .WithTags("files")
                .WithApiVersionSet(apiVersionSet)
                .UploadFileGroupItemEndpoint()
                .DeleteFileGroupItemEndpoint()
                .RequireAuthorization();
        }
    }
}
