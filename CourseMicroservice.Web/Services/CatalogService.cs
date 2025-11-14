using CourseMicroservice.Web.Pages.Instructor.ViewModels;
using CourseMicroservice.Web.Services.Refit;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CourseMicroservice.Web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService, ILogger<CatalogService> logger)
    {
        public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
        {
            var response = await catalogRefitService.GetCategoriesListAsync();
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while fetching categories: {Error}", problemDetails!.Title);
                return ServiceResult<List<CategoryViewModel>>.Error(problemDetails!);
            }

            var categories = response.Content!.Select(x => new CategoryViewModel(x.Id, x.Name)).ToList();
            return ServiceResult<List<CategoryViewModel>>.SuccessAsOk(categories);
        }
    }
}
