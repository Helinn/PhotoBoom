using System.Collections.Generic;
using MongoDB.Bson;
using PhotoBoom.Entities;

namespace PhotoBoom.Services
{
    public interface IGalleryService
    {
        bool AddPhoto(Photo request);

        ICollection<Photo> GetPhotos();

        Photo GetPhotoByFileName(string filename);

        Photo GetPhoto(int id);

    }
}