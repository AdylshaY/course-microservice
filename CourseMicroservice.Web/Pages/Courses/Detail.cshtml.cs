using CourseMicroservice.Web.PageModels;
using CourseMicroservice.Web.Pages.Instructor.ViewModels;
using CourseMicroservice.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservice.Web.Pages.Courses
{
    public class DetailModel(CatalogService catalogService) : BasePageModel
    {
        public CourseViewModel? Course { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var courseAsResult = await catalogService.GetCourse(id);

            if (courseAsResult.IsFail) return ErrorPage(courseAsResult);

            Course = courseAsResult.Data!;
            return Page();
        }
    }
}
