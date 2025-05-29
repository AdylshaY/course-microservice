using Asp.Versioning.Builder;
using CourseMicroservice.Catalog.API.Features.Courses.Create;
using CourseMicroservice.Catalog.API.Features.Courses.Delete;
using CourseMicroservice.Catalog.API.Features.Courses.GetAll;
using CourseMicroservice.Catalog.API.Features.Courses.GetById;
using CourseMicroservice.Catalog.API.Features.Courses.GetCourseListByUserId;
using CourseMicroservice.Catalog.API.Features.Courses.Update;

namespace CourseMicroservice.Catalog.API.Features.Courses
{
    public static class CourseEndpointExtension
    {
        public static void AddCourseGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses")
                .WithTags("Courses")
                .WithApiVersionSet(apiVersionSet)
                .CreateCourseGroupItemEndpoint()
                .GetAllCourseGroupItemEndpoint()
                .GetByIdCourseGroupItemEndpoint()
                .GetAllCourseListByUserIdGroupItemEndpoint()
                .UpdateCourseGroupItemEndpoint()
                .DeleteCourseGroupItemEndpoint();
        }
    }
}
