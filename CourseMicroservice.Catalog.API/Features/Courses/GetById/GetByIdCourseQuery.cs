namespace CourseMicroservice.Catalog.API.Features.Courses.GetById
{
    public record GetByIdCourseQuery(Guid Id) : IRequestByServiceResult<CourseDto>;
}
