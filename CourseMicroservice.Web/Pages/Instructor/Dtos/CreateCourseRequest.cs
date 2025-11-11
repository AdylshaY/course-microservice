namespace CourseMicroservice.Web.Pages.Instructor.Dtos
{
    public record CreateCourseRequest(
        string Name,
        string Description,
        decimal Price,
        IFormFile? Picture,
        Guid CategoryId
    );
}
