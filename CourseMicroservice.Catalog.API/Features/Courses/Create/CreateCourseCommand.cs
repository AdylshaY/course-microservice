namespace CourseMicroservice.Catalog.API.Features.Courses.Create
{
    public record CreateCourseCommand : IRequestByServiceResult<CreateCourseResponse>
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public decimal Price { get; init; }
        public IFormFile? Picture { get; init; }
        public Guid CategoryId { get; init; }
    }
}
