namespace CourseMicroservice.Catalog.API.Features.Courses.GetById
{
    public class GetByIdCourseQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetByIdCourseQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetByIdCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (course is null)
            {
                return ServiceResult<CourseDto>.Error("Course not found", $"The course with id ({request.Id}) was not found.", HttpStatusCode.NotFound);
            }

            var category = await context.Categories.FirstAsync(x => x.Id == course.CategoryId, cancellationToken);

            course.Category = category;

            var courseDto = mapper.Map<CourseDto>(course);
            return ServiceResult<CourseDto>.SuccessAsOk(courseDto);
        }
    }
}
