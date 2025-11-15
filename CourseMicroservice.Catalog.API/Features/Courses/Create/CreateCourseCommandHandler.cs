
using CourseMicroservice.Bus.Commands;
using CourseMicroservice.Shared.Services;

namespace CourseMicroservice.Catalog.API.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint, IIdentityService identityService) : IRequestHandler<CreateCourseCommand, ServiceResult<CreateCourseResponse>>
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
            newCourse.UserId = identityService.UserId;
            newCourse.Feature = new Feature()
            {
                Duration = 10, // Calculate by course video
                EducatorFullName = identityService.UserName,
                Rating = 0
            };

            context.Courses.Add(newCourse);
            await context.SaveChangesAsync(cancellationToken);

            if (request.Picture is not null)
            {
                using MemoryStream memoryStream = new MemoryStream();
                await request.Picture.CopyToAsync(memoryStream, cancellationToken);

                var pictureAsByteArray = memoryStream.ToArray();

                var uploadCoursePictureCommand = new UploadCoursePictureCommand(newCourse.Id, pictureAsByteArray, request.Picture.FileName);

                await publishEndpoint.Publish(uploadCoursePictureCommand, cancellationToken);
            }

            return ServiceResult<CreateCourseResponse>.SuccessAsCreated(new CreateCourseResponse(newCourse.Id), $"/api/courses/{newCourse.Id}");
        }
    }
}
