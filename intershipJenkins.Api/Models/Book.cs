using MongoDB.Bson.Serialization.Attributes;

namespace intershipJenkins.Api.Models
{
    public class Book : BaseEntity
    {
        [BsonElement("Name")]
        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;

        public string Author { get; set; } = null!;
    }
}
