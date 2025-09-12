namespace CourseMicroservice.Bus.Commands
{
    public record UploadCoursePictureCommand(Guid CourseId, byte[] Picture, string FileName);
}
