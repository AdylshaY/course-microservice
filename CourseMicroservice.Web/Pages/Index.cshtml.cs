namespace CourseMicroservice.Web.Pages
{
    using CourseMicroservice.Web.PageModels;
    using CourseMicroservice.Web.Pages.Instructor.ViewModels;
    using CourseMicroservice.Web.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class IndexModel(CatalogService catalogService) : BasePageModel
    {
        public List<CourseViewModel> Courses { get; set; } = [];

        public async Task<IActionResult> OnGet()
        {
            var courseList = await catalogService.GetAllCoursesAsync();
            if (courseList.IsFail) return ErrorPage(courseList);

            Courses = courseList.Data!;

            return Page();
        }
    }
}
