using MongoDB.Bson.Serialization.Attributes;

namespace CourseMicroservice.Catalog.API.Repositories
{
    public class BaseEntity
    {
        // snow flake
        [BsonElement("_id")]
        public Guid Id { get; set; }
    }
}
