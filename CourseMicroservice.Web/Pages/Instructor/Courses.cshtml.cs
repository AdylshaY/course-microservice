using CourseMicroservice.Web.Pages.Instructor.ViewModels;
using CourseMicroservice.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseMicroservice.Web.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : PageModel
    {
        public List<CourseViewModel> CourseViewModels { get; set; } = null!;

        public async Task OnGetAsync()
        {
            var result = await catalogService.GetCourseListByUserId();
            if (result.IsFail && result.Data is null)
            {
                //TODO : redirect error page
            }

            CourseViewModels = result.Data!;
        }

        public async Task<IActionResult> OnGetDeleteAsync(Guid id)
        {
            var result = await catalogService.DeleteCourseByCourseIdAsync(id);
            if (result.IsFail)
            {
                //TODO : redirect error page
            }
            return RedirectToPage();
        }
    }
}
