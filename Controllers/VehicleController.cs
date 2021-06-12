using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Models;
using Vega.Models.DataTransferObjects;
using Vega.Persistence;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly VegaDbContext _vegaDbContext;
        public readonly IMapper _mapper;
        public readonly IVehicleRepository _vehicleRepository;

        public VehicleController(VegaDbContext context, IMapper mapper, IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _vegaDbContext = context;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleDTO>> Get()
        {
            var vehicles = await _vehicleRepository.GetVehicles();
            return _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDTO>> Get(long id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id);

            if (vehicle == default)
            {
                return BadRequest("Vehicle not found");
            }

            var result = _mapper.Map<VehicleDTO>(vehicle);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDTO>> Insert(SaveVehicleDTO vehicleDTO)
        {
            if (vehicleDTO.Id != 0)
            {
                return BadRequest("Id cannot be set for the insert action");
            }

            var vehicle = _mapper.Map<Vehicle>(vehicleDTO);
            vehicle.LastUpdate = DateTime.Now;
            await _vegaDbContext.Vehicles.AddAsync(vehicle);

            try
            {
                await _vegaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return NotFound("SaveChanges fail.");
            }

            vehicle = await _vehicleRepository.GetVehicle(vehicle.Id);
            var result = _mapper.Map<VehicleDTO>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleDTO>> Update(long id, SaveVehicleDTO vehicleDTO)
        {
            var vehicle = await _vegaDbContext.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id.Equals(id));
            if (vehicle == default)
            {
                return BadRequest("Vehicle not found");
            }

            _mapper.Map(vehicleDTO, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            try
            {
                await _vegaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return NotFound("SaveChanges fail.");
            }

            vehicle = await _vehicleRepository.GetVehicle(vehicle.Id);
            var result = _mapper.Map<VehicleDTO>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<long>> Delete(long id)
        {
            var vehicle = await _vegaDbContext.Vehicles.FindAsync(id);
            if (vehicle == default)
            {
                return BadRequest("Vehicle not found");
            }

            _vegaDbContext.Vehicles.Remove(vehicle);

            try
            {
                await _vegaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return NotFound("SaveChanges fail");
            }

            return Ok(id);
        }
    }
}