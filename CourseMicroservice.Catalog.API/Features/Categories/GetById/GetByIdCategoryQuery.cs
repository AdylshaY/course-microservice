namespace CourseMicroservice.Catalog.API.Features.Categories.GetById
{
    public record GetByIdCategoryQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;
}
