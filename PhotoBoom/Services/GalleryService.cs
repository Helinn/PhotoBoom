using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using PhotoBoom.Entities;
using PhotoBoom.Repositories;

namespace PhotoBoom.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository)
        {
            this.galleryRepository = galleryRepository;
        }

        public bool AddPhoto(Photo request)
        {
            galleryRepository.Add(request);
            return true;
        }

        public Photo GetPhotoByFileName(string filename)
        {
            throw new NotImplementedException();
        }

        public Photo GetPhoto(int id)
        {
            FilterDefinition<Photo> filter = Builders<Photo>.Filter.Eq("_id", id);

            return  galleryRepository.GetByFilter(filter).Result.FirstOrDefault();
        }

        public ICollection<Photo> GetPhotos()
        {
            return galleryRepository.Get();
        }

        /*public bool AddProduct(AddProductRequest product) {
            var p = new Product() { Id = product.Id, Name = product.Name, Price = product.Price, AvailableQuantity = product.Quantity };
            productRepository.Add(p);
            return true;
        }*/
    }
}