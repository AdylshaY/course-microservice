using CourseMicroservice.Web.Pages.Instructor.ViewModels;
using CourseMicroservice.Web.Services.Refit;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Text.Json;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

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

        public async Task<ServiceResult> CreateCourseAsync(CreateCourseViewModel model)
        {
            StreamPart? pictureStreamPart = null;
            await using var stream = model.PictureFormFile?.OpenReadStream();

            if (model.PictureFormFile is not null && model.PictureFormFile.Length > 0)
            {
                pictureStreamPart = new StreamPart(stream!, model.PictureFormFile.FileName, model.PictureFormFile.ContentType);
            }

            var response = await catalogRefitService.CreateCourseAsync(model.Name, model.Description, model.Price, pictureStreamPart, model.CategoryId.ToString()!);

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while creating course: {Error}", problemDetails!.Title);
                return ServiceResult.Error(problemDetails!);
            }

            return ServiceResult.Success();
        }
    }
}
