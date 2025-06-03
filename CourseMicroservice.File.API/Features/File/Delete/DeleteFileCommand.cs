namespace CourseMicroservice.File.API.Features.File.Delete
{
    public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
}
