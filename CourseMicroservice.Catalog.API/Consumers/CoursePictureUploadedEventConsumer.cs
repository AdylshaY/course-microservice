using CourseMicroservice.Bus.Events;

namespace CourseMicroservice.Catalog.API.Consumers
{
    public class CoursePictureUploadedEventConsumer(IServiceProvider serviceProvider) : IConsumer<CoursePictureUploadedEvent>
    {
        public async Task Consume(ConsumeContext<CoursePictureUploadedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var course = await dbContext.Courses.FindAsync(context.Message.CourseId) ?? throw new NotImplementedException();

            course.ImageUrl = context.Message.ImageUrl;
            await dbContext.SaveChangesAsync();
        }
    }
}
