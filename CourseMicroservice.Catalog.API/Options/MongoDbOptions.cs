using System.ComponentModel.DataAnnotations;

namespace CourseMicroservice.Catalog.API.Options
{
    public class MongoDbOptions
    {
        [Required]
        public string DatabaseName { get; set; } = default!;

        [Required]
        public string ConnectionString { get; set; } = default!;
    }
}
