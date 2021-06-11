using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Models;
using Vega.Models.DataTransferObjects;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        public VegaDbContext _vegaDbContext { get; }
        public IMapper _mapper { get; }

        public VehicleController(VegaDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _vegaDbContext = context;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleDTO>> Get()
        {
            var vehicles = await _vegaDbContext.Vehicles.Include(f => f.Features).ToListAsync();

            return _mapper.Map<IEnumerable<VehicleDTO>>(vehicles);
        }

        [HttpGet("{id}", Name = "FirstAsync")]
        public async Task<ActionResult<VehicleDTO>> Get(long Id)
        {
            var vehicle = await _vegaDbContext.Vehicles.Include(f => f.Features).FirstAsync(f => f.Id.Equals(Id));

            if (vehicle != default)
            {
                return Ok(_mapper.Map<VehicleDTO>(vehicle));
            }

            return NotFound($"Vehicle with Id {Id} was not found.");
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDTO>> Insert(VehicleDTO vehicleDTO)
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

            var result = _mapper.Map<VehicleDTO>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleDTO>> Update(long id, VehicleDTO vehicleDTO)
        {
            var vehicleToUpdate = await _vegaDbContext.Vehicles.Include(f => f.Features).SingleOrDefaultAsync(v => v.Id.Equals(id));
            if (vehicleToUpdate == null)
            {
                return BadRequest("Vehicle not found");
            }

            _mapper.Map(vehicleDTO, vehicleToUpdate);
            vehicleToUpdate.LastUpdate = DateTime.Now;

            try
            {
                await _vegaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return NotFound("SaveChanges fail.");
            }

            var result = _mapper.Map<VehicleDTO>(vehicleToUpdate);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleDTO>> Delete(long id)
        {
            var vehicleToDelete = await _vegaDbContext.Vehicles.FindAsync(id);
            if (vehicleToDelete == null)
            {
                return BadRequest("Vehicle not found");
            }

            _vegaDbContext.Vehicles.Remove(vehicleToDelete);

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