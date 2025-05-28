namespace CourseMicroservice.Catalog.API.Features.Courses.Dtos
{
    public record CourseDto(Guid Id, string Name, string Description, string ImageUrl, decimal Price, CategoryDto Category, FeatureDto Feature, Guid UserId);
}
