using MongoDB.Bson.Serialization.Attributes;

namespace CourseMicroservice.Discount.API.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }
    }
}
