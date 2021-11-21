using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vega.Controllers.DataTransferObjects;
using Vega.Core;
using Vega.Core.Models;
using VegaApp.Core;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/vehicle/{vehicleId}/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly PhotoSettings _photoSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;

        public PhotoController(IWebHostEnvironment webHostEnvironment,
                                IMapper mapper,
                                IOptionsSnapshot<PhotoSettings> options,
                                IUnitOfWork unitOfWork,
                                IPhotoService photoService)
        {
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _photoSettings = options.Value;
            _unitOfWork = unitOfWork;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoDTO>> GetPhotos(int vehicleId)
        {
            var photos = await _unitOfWork.Photos.GetPhotos(vehicleId);

            return _mapper.Map<IEnumerable<PhotoDTO>>(photos);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upload(long vehicleId, IFormFile file)
        {
            var vehicle = await _unitOfWork.Vehicles.Get(vehicleId);

            if (vehicle == default) return BadRequest($"Could not find vehicle with {vehicleId}.");
            if (file == null) return BadRequest("No file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Max file size exceeded");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");

            var uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var photo = await _photoService.UploadPhoto(vehicle, file, uploadDirectory);

            return Ok(_mapper.Map<PhotoDTO>(photo));
        }
    }
}