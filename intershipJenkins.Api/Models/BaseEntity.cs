using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace intershipJenkins.Api.Models
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; }

        public DateTime DeletedAt { get; set; }

        public bool Enabeled { get; set; } = true;
    }
}
