using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PhotoBoom.Entities
{
    public class Photo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string FileName { get; set; }
        public string PictureDataAsString { get; set; }
    }
}
