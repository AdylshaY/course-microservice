
namespace CourseMicroservice.Catalog.API.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<CreateCourseResponse>>
    {
        public async Task<ServiceResult<CreateCourseResponse>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

            if (!hasCategory)
            {
                return ServiceResult<CreateCourseResponse>.Error("Category not found.", $"The category with id({request.CategoryId}) was not found.", HttpStatusCode.NotFound);
            }

            var hasCourse = await context.Courses.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (hasCourse)
            {
                return ServiceResult<CreateCourseResponse>.Error("Course already exists.", $"The course with name({request.Name}) already exists.", HttpStatusCode.BadRequest);
            }

            var newCourse = mapper.Map<Course>(request);
            newCourse.Created = DateTime.Now;
            newCourse.Id = NewId.NextSequentialGuid();
            newCourse.Feature = new Feature()
            {
                Duration = 10, // Calculate by course video
                EducatorFullName = "Adylsha Yumayev", // Get by token payload
                Rating = 0
            };

            context.Courses.Add(newCourse);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCourseResponse>.SuccessAsCreated(new CreateCourseResponse(newCourse.Id), $"/api/courses/{newCourse.Id}");
        }
    }
}
