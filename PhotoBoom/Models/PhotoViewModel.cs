using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PhotoBoom.Entities;
    
namespace PhotoBoom.Models
{
    public class PhotoViewModel
    {
        [Required]  
        [Display(Name = "Upload File")] 
        public IFormFile FileAttach { get; set; }  
  
        /// <summary>  
        /// Gets or sets Image file list.  
        /// </summary>  
        public IEnumerable<Photo> PhotoList { get; set; }  
    }
}
