using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vega.Core.Models;

namespace VegaApp.Core
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadDirectory);
    }
}