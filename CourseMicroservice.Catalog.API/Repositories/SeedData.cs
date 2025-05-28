using CourseMicroservice.Catalog.API.Features.Categories;
using CourseMicroservice.Catalog.API.Features.Courses;

namespace CourseMicroservice.Catalog.API.Repositories
{
    public static class SeedData
    {
        public static async Task AddSeedDataExtension(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

                if (!dbContext.Categories.Any())
                {
                    var categoryList = new List<Category>()
                    {
                        new() { Id = NewId.NextSequentialGuid(), Name = "Development"},
                        new() { Id = NewId.NextSequentialGuid(), Name = "Business"},
                        new() { Id = NewId.NextSequentialGuid(), Name = "IT & Software"},
                        new() { Id = NewId.NextSequentialGuid(), Name = "Office Productivity"},
                        new() { Id = NewId.NextSequentialGuid(), Name = "Personal Development"}
                    };

                    await dbContext.Categories.AddRangeAsync(categoryList);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Courses.Any())
                {
                    var category = await dbContext.Categories.FirstAsync();
                    var randomUserId = Guid.NewGuid();

                    var courseList = new List<Course>()
                    {
                        new()
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "C#",
                            Description = "C# Course",
                            Price = 149.99M,
                            UserId = randomUserId,
                            CategoryId = category.Id,
                            Created = DateTime.Now,
                            Feature = new Feature()
                            {
                                EducatorFullName = "Adylsha Yumayev",
                                Duration = 27,
                                Rating = 3.4F
                            },
                        },
                        new()
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "React.js",
                            Description = "React.js Course",
                            Price = 99.99M,
                            UserId = randomUserId,
                            CategoryId = category.Id,
                            Created = DateTime.Now,
                            Feature = new Feature()
                            {
                                EducatorFullName = "Adylsha Yumayev",
                                Duration = 14,
                                Rating = 4.8F
                            },
                        },
                        new()
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "Python",
                            Description = "Python Course",
                            Price = 129.99M,
                            UserId = Guid.NewGuid(),
                            CategoryId = category.Id,
                            Created = DateTime.Now,
                            Feature = new Feature()
                            {
                                EducatorFullName = "John Doe",
                                Duration = 12,
                                Rating = 4.3F
                            },
                        },
                        new()
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "Java",
                            Description = "Java Course",
                            Price = 249.99M,
                            UserId = Guid.NewGuid(),
                            CategoryId = category.Id,
                            Created = DateTime.Now,
                            Feature = new Feature()
                            {
                                EducatorFullName = "Martin Luther",
                                Duration = 34,
                                Rating = 4.9F
                            },
                        },
                    };

                    await dbContext.Courses.AddRangeAsync(courseList);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
