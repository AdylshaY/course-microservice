using CourseMicroservice.Catalog.API.Features.Courses.GetAllByUserId;

namespace CourseMicroservice.Catalog.API.Features.Courses.GetCourseListByUserId
{
    public class GetAllCourseListByUserIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCourseListByUserIdQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCourseListByUserIdQuery request, CancellationToken cancellationToken)
        {
            var courseList = await context.Courses.Where(x => x.UserId == request.Id).ToListAsync(cancellationToken);
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
