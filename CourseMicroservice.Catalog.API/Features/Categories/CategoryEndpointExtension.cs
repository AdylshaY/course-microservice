using Asp.Versioning.Builder;
using CourseMicroservice.Catalog.API.Features.Categories.Create;
using CourseMicroservice.Catalog.API.Features.Categories.Delete;
using CourseMicroservice.Catalog.API.Features.Categories.GetAll;
using CourseMicroservice.Catalog.API.Features.Categories.GetById;
using CourseMicroservice.Catalog.API.Features.Categories.Update;

namespace CourseMicroservice.Catalog.API.Features.Categories
{
    public static class CategoryEndpointExtension
    {
        public static void AddCategoryGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/categories")
                .WithTags("Categories")
                .WithApiVersionSet(apiVersionSet)
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint()
                .GetByIdCategoryGroupItemEndpoint()
                .UpdateCategoryGroupItemEndpoint()
                .DeleteCategoryGroupItemEndpoint();
        }
    }
}
