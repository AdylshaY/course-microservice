
namespace CourseMicroservice.Catalog.API.Features.Courses.Update
{
    public class UpdateCourseCommandHandler(AppDbContext context) : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var existingCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existingCourse is null)
            {
                return ServiceResult.ErrorAsNotFound();
            }

            existingCourse.Name = request.Name;
            existingCourse.Description = request.Description;
            existingCourse.Price = request.Price;
            existingCourse.ImageUrl = request.ImageUrl;
            existingCourse.CategoryId = request.CategoryId;

            context.Courses.Update(existingCourse);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
