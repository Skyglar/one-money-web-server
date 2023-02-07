using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace domain.Entities
{
    public abstract class EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid NetUid { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public bool Deleted { get; set; }
    }
}
