using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vega.Controllers.DataTransferObjects;
using Vega.Core;
using Vega.Core.Models;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<QueryResultDTO<VehicleDTO>> Get([FromQuery] VehicleQueryDTO vehicleQueryDTO)
        {
            var vehicleQuery = _mapper.Map<VehicleQuery>(vehicleQueryDTO);
            var queryResult = await _unitOfWork.Vehicles.QueryAll(vehicleQuery);
            return _mapper.Map<QueryResultDTO<VehicleDTO>>(queryResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDTO>> Get(long id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetWithRelated(id);

            if (vehicle == default)
            {
                return BadRequest("Vehicle not found");
            }

            var result = _mapper.Map<VehicleDTO>(vehicle);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<VehicleDTO>> Insert(SaveVehicleDTO vehicleDTO)
        {
            if (vehicleDTO.Id != 0)
            {
                return BadRequest("Id cannot be set for the insert action");
            }

            var vehicle = _mapper.Map<Vehicle>(vehicleDTO);
            vehicle.LastUpdate = DateTime.Now;
            await _unitOfWork.Vehicles.AddAsync(vehicle);
            await _unitOfWork.Complete();

            vehicle = await _unitOfWork.Vehicles.GetWithRelated(vehicle.Id);
            var result = _mapper.Map<VehicleDTO>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<ActionResult<VehicleDTO>> Update(long id, SaveVehicleDTO vehicleDTO)
        {
            var vehicle = await _unitOfWork.Vehicles.GetWithFeatures(id);
            if (vehicle == default)
            {
                return BadRequest("Vehicle not found");
            }

            _mapper.Map(vehicleDTO, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await _unitOfWork.Complete();

            vehicle = await _unitOfWork.Vehicles.GetWithRelated(vehicle.Id);
            var result = _mapper.Map<VehicleDTO>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<long>> Delete(long id)
        {
            var vehicle = await _unitOfWork.Vehicles.Get(id);
            if (vehicle == default)
            {
                return BadRequest("Vehicle not found");
            }

            _unitOfWork.Vehicles.Remove(vehicle);
            await _unitOfWork.Complete();

            return Ok(id);
        }
    }
}