using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using PhotoBoom.Entities;
using PhotoBoom.Models;
using PhotoBoom.Services;

namespace PhotoBoom.Controllers
{
    public class GalleryController : Controller
    {
        public IGalleryService galleryService { get; set; }
        public GalleryController(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public IActionResult Index()
        {
            PhotoViewModel model = new PhotoViewModel { FileAttach = null, PhotoList = new List<Photo>() };  
            
            model.PhotoList = galleryService.GetPhotos();
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
        [HttpPost]  
        [AllowAnonymous]  
        [ValidateAntiForgeryToken]  
        public ActionResult Index(PhotoViewModel model)  
        {  
            // Initialization.  
            string fileContent = string.Empty;  
            string fileContentType = string.Empty;  
  
            try  
            {  
                // Verification  
                if (ModelState.IsValid)  
                {  
                    // Converting to bytes.  
                    byte[] thePictureAsBytes = new byte[model.FileAttach.Length];  

                using (var memoryStream = new MemoryStream())
                {
                    model.FileAttach.CopyToAsync(memoryStream);
                    thePictureAsBytes = memoryStream.ToArray();

                }
                   string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);  
  
                //create a new mongo picture model object to insert into the db  
                Photo thePicture = new Photo()  
                {  
                    FileName = model.FileAttach.FileName,  
                    PictureDataAsString = thePictureDataAsString  
                };  
  
                //insert the picture object  
                var didItInsert = galleryService.AddPhoto(thePicture);
  
                if (didItInsert)  
                    ViewBag.Message = "The image was updated successfully";  
                else  
                    ViewBag.Message = "A database error has occured";  
                }  
 
                
            }  
            catch (Exception ex)  
            {  
                // Info  
                Console.Write(ex);  
            }  
  
            // Info  
            return this.View(model);  
        }  
        [HttpPost]  
        public async Task<ActionResult> AddPictureAsync(PhotoViewModel model)  
        {  
            if (model.FileAttach.Length > 0)  
            {  
                //get the file's name  
                string theFileName = Path.GetFileName(model.FileAttach.FileName);  
  
                //get the bytes from the content stream of the file  
                byte[] thePictureAsBytes = new byte[model.FileAttach.Length];  

                using (var memoryStream = new MemoryStream())
                {
                    await model.FileAttach.CopyToAsync(memoryStream);
                    thePictureAsBytes = memoryStream.ToArray();

                }
                //convert the bytes of image data to a string using the Base64 encoding  
                string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);  
  
                //create a new mongo picture model object to insert into the db  
                Photo thePicture = new Photo()  
                {  
                    FileName = theFileName,  
                    PictureDataAsString = thePictureDataAsString  
                };  
  
                //insert the picture object  
                var didItInsert = galleryService.AddPhoto(thePicture);
  
                if (didItInsert)  
                    ViewBag.Message = "The image was updated successfully";  
                else  
                    ViewBag.Message = "A database error has occured";  
            }  
            else  
                ViewBag.Message = "You must upload an image";  
  
            return View();  
        }  
    
        public ActionResult DownloadFile(string fileId)  
        {  
            // Model binding.  
            PhotoViewModel model = new PhotoViewModel { FileAttach = null, PhotoList = new List<Photo>() };  

            var fileInfo = galleryService.GetPhoto(fileId);
            // Info.  
            return this.GetFile(fileInfo.PictureDataAsString, "image/png");  

        } 

        private FileResult GetFile(string fileContent, string fileContentType)  
        {  
            // Initialization.  
            FileResult file = null;  
  
            try  
            {  
                // Get file.  
                byte[] byteContent = Convert.FromBase64String(fileContent);  
                file = this.File(byteContent, fileContentType);  
            }  
            catch (Exception ex)  
            {  
                // Info.  
                throw ex;  
            }  
  
            // info.  
            return file;  
        }   
    
    }
}
