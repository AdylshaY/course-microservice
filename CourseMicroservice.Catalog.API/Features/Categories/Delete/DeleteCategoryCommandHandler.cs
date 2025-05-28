
namespace CourseMicroservice.Catalog.API.Features.Categories.Delete
{
    public class DeleteCategoryCommandHandler(AppDbContext context) : IRequestHandler<DeleteCategoryCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (category is null) return ServiceResult.ErrorAsNotFound();

            var isHasCourse = await context.Courses.AnyAsync(x => x.CategoryId == request.Id, cancellationToken);

            if (isHasCourse) return ServiceResult.Error($"This category ({category.Name}) could not be deleted because there are courses belonging to this category.", "To delete this category, you must first delete the courses belonging to this category.", HttpStatusCode.BadRequest);

            context.Categories.Remove(category);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
