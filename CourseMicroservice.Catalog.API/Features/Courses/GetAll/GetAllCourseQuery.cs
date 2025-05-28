using CourseMicroservice.Catalog.API.Features.Courses.Dtos;

namespace CourseMicroservice.Catalog.API.Features.Courses.GetAll
{
    public record GetAllCourseQuery() : IRequestByServiceResult<List<CourseDto>>;
}
