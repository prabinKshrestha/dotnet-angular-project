using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Model
{
    public interface IImageModel
    {
        string ImageName { get; set; }
        string FilePath { get; }
        string Image { get; }
    }
    public interface IImageAddModel
    {
        IFormFile ImageFile { get; set; }
        string FilePath { get; }
        bool IsImageRequired { get; }
    }
    public interface IImageUpdateModel
    {
        IFormFile ImageFile { get; set; }
        string FilePath { get; }
        bool IsImageRequired { get;}
        bool IsImageChanged { get; set; }
    }
    public class ImageModel
    {
        public ImageModel(IFormFile imageFile, string filePath, string imageName)
        {
            FilePath = filePath;
            ImageName = imageName;
            ImageFile = imageFile;
        }
        public string FilePath { get; set; }
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
