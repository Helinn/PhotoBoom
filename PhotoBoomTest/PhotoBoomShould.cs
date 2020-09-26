using System.Collections.Generic;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using PhotoBoom.Entities;
using PhotoBoom.Repositories;
using PhotoBoom.Services;

namespace PhotoBoomTest
{
    public class PhotoBoomShould
    {
        private readonly ICollection<Photo> _PhotoList;
        
        public PhotoBoomShould()
        {

            _PhotoList = new List<Photo>(){
                new Photo() { Id =  "123", FileName = "test.png", PictureDataAsString = ""}
            };
        }
 

        [Test]
        public void GetPhotos_Should_Return_A_List()
        {
            var galleryRepositoryMock = new Mock<IGalleryRepository>();
            var service = new GalleryService(galleryRepositoryMock.Object);

            galleryRepositoryMock.Setup(x => x.Get()).Returns(_PhotoList);

            var photos = service.GetPhotos();
            Assert.NotNull(photos);
        }
    }
}