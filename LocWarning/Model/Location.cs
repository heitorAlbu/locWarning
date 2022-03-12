using MongoDB.Bson.Serialization.Attributes;

namespace LocWarning.Model
{
    public class Location
    {
        [BsonId]
        [BsonRepresentationAttribute(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
