using CourseMicroservice.Web.Extensions;
using CourseMicroservice.Web.Pages.Instructor.ViewModels;
using CourseMicroservice.Web.Services.Refit;
using Refit;

namespace CourseMicroservice.Web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService, ILogger<CatalogService> logger, UserService userService)
    {
        public async Task<ServiceResult<CourseViewModel>> GetCourse(Guid courseId)
        {
            var response = await catalogRefitService.GetCourse(courseId);

            if (!response.IsSuccessStatusCode)
                return ServiceResult<CourseViewModel>.FailFromProblemDetails(response.Error);

            var course = response.Content!;
            var courseViewModel = new CourseViewModel(course.Id, course.Name, course.Description, course.Price,
                course.ImageUrl, course.Created.ToLongDateString(), course.Feature.EducatorFullName, course.Category.Name,
                course.Feature.Duration,
                course.Feature.Rating);

            return ServiceResult<CourseViewModel>.Success(courseViewModel);
        }

        public async Task<ServiceResult<List<CourseViewModel>>> GetAllCoursesAsync()
        {
            var coursesAsResult = await catalogRefitService.GetAllCourses();

            if (!coursesAsResult.IsSuccessStatusCode)
            {
                logger.LogProblemDetails(coursesAsResult.Error);

                return ServiceResult<List<CourseViewModel>>.Error(
                    "Failed to retrieve course data. Please try again later.");
            }

            var courses = coursesAsResult.Content!;

            var categoriesViewModel = courses.Select(c =>
                new CourseViewModel(c.Id, c.Name, c.Description, c.Price, c.ImageUrl, c.Created.ToLongDateString(),
                    c.Feature.EducatorFullName, c.Category.Name, c.Feature.Duration,
                    c.Feature.Rating)).ToList();

            return ServiceResult<List<CourseViewModel>>.Success(categoriesViewModel);
        }

        public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
        {
            var response = await catalogRefitService.GetCategoriesListAsync();
            if (!response.IsSuccessStatusCode)
            {
                logger.LogProblemDetails(response.Error);
                return ServiceResult<List<CategoryViewModel>>.Error("Failed to retrieve category data. Please try again later.");
            }

            var categories = response.Content!.Select(x => new CategoryViewModel(x.Id, x.Name)).ToList();
            return ServiceResult<List<CategoryViewModel>>.Success(categories);
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
                logger.LogProblemDetails(response.Error);
                return ServiceResult.Error("Failed to create course. Please try again later.");
            }

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<CourseViewModel>>> GetCourseListByUserId()
        {
            var courseList = await catalogRefitService.GetCourseListByUserId(userService.UserId);

            if (!courseList.IsSuccessStatusCode)
            {
                logger.LogProblemDetails(courseList.Error);
                return ServiceResult<List<CourseViewModel>>.Error("Failed to retrieve course data. Please try again later.");
            }

            var courses = courseList.Content!.Select(x => new CourseViewModel(
                x.Id,
                x.Name,
                x.Description,
                x.Price,
                x.ImageUrl,
                x.Created.ToLongDateString(),
                x.Feature.EducatorFullName,
                x.Category.Name,
                x.Feature.Duration,
                x.Feature.Rating
            )).ToList();

            return ServiceResult<List<CourseViewModel>>.Success(courses);
        }

        public async Task<ServiceResult> DeleteCourseByCourseIdAsync(Guid courseId)
        {
            var response = await catalogRefitService.DeleteCourseAsync(courseId);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogProblemDetails(response.Error);
                return ServiceResult.Error("Failed to delete course. Please try again later.");
            }
            return ServiceResult.Success();
        }
    }
}
