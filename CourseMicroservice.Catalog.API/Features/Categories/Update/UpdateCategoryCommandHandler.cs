
namespace CourseMicroservice.Catalog.API.Features.Categories.Update
{
    public class UpdateCategoryCommandHandler(AppDbContext context) : IRequestHandler<UpdateCategoryCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (category is null) return ServiceResult.ErrorAsNotFound();

            category.Name = request.Name;

            context.Categories.Update(category);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
