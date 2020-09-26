using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PhotoBoom.Entities
{
    public class Photo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FileName { get; set; }

        public string PictureDataAsString { get; set; }
        
        public ICollection<string> TagList { get; set; }
    }
}
