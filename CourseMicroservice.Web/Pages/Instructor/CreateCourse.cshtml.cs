using CourseMicroservice.Web.Pages.Instructor.ViewModels;
using CourseMicroservice.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseMicroservice.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class CreateCourseModel(CatalogService catalogService) : PageModel
    {
        [BindProperty]
        public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;

        public async Task OnGetAsync()
        {
            var categoriesResult = await catalogService.GetCategoriesAsync();
            if (categoriesResult.IsFail || categoriesResult.Data is null)
            {
                //TODO: Handle error (e.g., log it, show a message to the user, etc.)
                return;
            }

            ViewModel.SetCategoryDropdownList(categoriesResult.Data);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await catalogService.CreateCourseAsync(ViewModel);
            if (result.IsFail)
            {
                //TODO: Handle error (e.g., log it, show a message to the user, etc.)
            }

            return RedirectToPage("Courses");
        }
    }
}
