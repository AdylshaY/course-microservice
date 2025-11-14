using CourseMicroservice.Web.Pages.Instructor.ViewModels;
using CourseMicroservice.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseMicroservice.Web.Pages.Instructor
{
    public class CreateCourseModel(CatalogService catalogService) : PageModel
    {
        public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;
        public async Task OnGet()
        {
            var categoriesResult = await catalogService.GetCategoriesAsync();
            if (categoriesResult.IsFail || categoriesResult.Data is null)
            {
                //TODO: Handle error (e.g., log it, show a message to the user, etc.)
                return;
            }

            ViewModel.SetCategoryDropdownList(categoriesResult.Data);
        }
    }
}
