using CourseMicroservice.Catalog.API.Features.Courses;
using CourseMicroservice.Catalog.API.Repositories;

namespace CourseMicroservice.Catalog.API.Features.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;

        public List<Course>? Courses { get; set; }
    }
}
