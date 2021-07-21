using System;
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

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/vehicle/{vehicleId}/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;

        public PhotoController(IWebHostEnvironment webHostEnvironment, IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
            this.photoSettings = options.Value;
            this.vehicleRepository = vehicleRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(long vehicleId, IFormFile file)
        {
            var vehicle = await vehicleRepository.Get(vehicleId);

            if (vehicle == default) return BadRequest($"Could not find vehicle with {vehicleId}.");
            if (file == null) return BadRequest("No file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Max file size exceeded");
            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");

            var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
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
            await unitOfWork.Complete();

            return Ok(mapper.Map<PhotoDTO>(photo));
        }
    }
}