namespace CourseMicroservice.Catalog.API.Features.Categories.GetById
{
    public class GetByIdCategoryQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetByIdCategoryQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FindAsync(request.Id, cancellationToken);

            if (category is null) return ServiceResult<CategoryDto>.Error("Category not found", $"The category with Id({request.Id}) was not found", HttpStatusCode.NotFound);

            var categoryDto = mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.SuccessAsOk(categoryDto);
        }
    }
}
