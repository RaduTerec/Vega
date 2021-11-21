using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vega.Core;
using Vega.Core.Models;

namespace VegaApp.Core
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoStorage _photoStorage;
        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
        {
            this._photoStorage = photoStorage;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadDirectory)
        {
            var fileName = await _photoStorage.StorePhoto(uploadDirectory, file);

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await _unitOfWork.Complete();

            return photo;
        }
    }
}