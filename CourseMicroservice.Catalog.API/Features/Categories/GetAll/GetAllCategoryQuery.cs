namespace CourseMicroservice.Catalog.API.Features.Categories.GetAll
{
    public record GetAllCategoryQuery : IRequestByServiceResult<List<CategoryDto>>;
}
