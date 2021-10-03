using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vega.Controllers.DataTransferObjects;
using Vega.Core;
using Vega.Core.Models;
using Vega.Core.Repositories;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/vehicle/{vehicleId}/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly PhotoSettings _photoSettings;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoRepository _photoRepository;

        public PhotoController(IWebHostEnvironment webHostEnvironment,
                                IMapper mapper,
                                IOptionsSnapshot<PhotoSettings> options,
                                IVehicleRepository vehicleRepository,
                                IPhotoRepository photoRepository,
                                IUnitOfWork unitOfWork)
        {
            _photoRepository = photoRepository;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _photoSettings = options.Value;
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoDTO>> GetPhotos(int vehicleId)
        {
            var photos = await _photoRepository.GetPhotos(vehicleId);

            return _mapper.Map<IEnumerable<PhotoDTO>>(photos);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(long vehicleId, IFormFile file)
        {
            var vehicle = await _vehicleRepository.Get(vehicleId);

            if (vehicle == default) return BadRequest($"Could not find vehicle with {vehicleId}.");
            if (file == null) return BadRequest("No file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Max file size exceeded");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");

            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await _unitOfWork.Complete();

            return Ok(_mapper.Map<PhotoDTO>(photo));
        }
    }
}