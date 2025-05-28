using CourseMicroservice.Catalog.API.Features.Courses.Dtos;

namespace CourseMicroservice.Catalog.API.Features.Courses.GetAll
{
    public class GetAllCourseQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCourseQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
            var courseList = await context.Courses.ToListAsync(cancellationToken);
            var categoryList = await context.Categories.ToListAsync(cancellationToken);

            foreach (var course in courseList)
            {
                course.Category = categoryList.First(x => x.Id == course.CategoryId);
            }

            var courseDtoList = mapper.Map<List<CourseDto>>(courseList);
            return ServiceResult<List<CourseDto>>.SuccessAsOk(courseDtoList);
        }
    }
}
