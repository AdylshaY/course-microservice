using CourseMicroservice.Web.Pages.Instructor.ViewModels;
using CourseMicroservice.Web.Services;
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
    }
}
