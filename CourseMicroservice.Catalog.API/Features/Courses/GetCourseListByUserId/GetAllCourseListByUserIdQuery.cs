namespace CourseMicroservice.Catalog.API.Features.Courses.GetAllByUserId
{
    public record GetAllCourseListByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;
}
