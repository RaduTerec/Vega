using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace VegaApp.Core
{
    public interface IPhotoStorage
    {
         Task<string> StorePhoto(string uploadDirectory, IFormFile file);
    }
}