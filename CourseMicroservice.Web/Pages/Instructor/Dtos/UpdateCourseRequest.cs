namespace CourseMicroservice.Web.Pages.Instructor.Dtos
{
    public record UpdateCourseRequest(Guid Id, string Name, string Description, decimal Price, string? ImageUrl, Guid CategoryId);
}
