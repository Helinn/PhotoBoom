using System;
using MongoDB.Bson;

namespace PhotoBoom.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string PictureDataAsString { get; set; }
    }
}
