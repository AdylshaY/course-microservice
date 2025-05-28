namespace CourseMicroservice.Catalog.API.Features.Categories.Delete
{
    public record DeleteCategoryCommand(Guid Id) : IRequestByServiceResult;
}
