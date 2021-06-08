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

            // TODO: Delete this ugly code after rewriting the reverse mapping
            vehicle.Features.Clear();
            var features = await _vegaDbContext.Features.Where(f => vehicleDTO.Features.Contains(f.Id)).ToListAsync();
            foreach (var feature in features)
            {
                vehicle.Features.Add(feature);
            }

            await _vegaDbContext.Vehicles.AddAsync(vehicle);
            await _vegaDbContext.SaveChangesAsync();
            var result = _mapper.Map<VehicleDTO>(vehicle);

            if (result != default)
            {
                return Ok(result);
            }

            return BadRequest("Vehicle could not be created");
        }

        [HttpPut]
        public async Task<ActionResult<VehicleDTO>> Update(VehicleDTO vehicleDTO)
        {
            if (vehicleDTO.Id == 0)
            {
                return BadRequest("Id should be set for the update action");
            }

            var vehicleToUpdate = await _vegaDbContext.Vehicles.Include(f => f.Features).FirstAsync(v => v.Id.Equals(vehicleDTO.Id));
            vehicleToUpdate.ContactName = vehicleDTO.Contact.Name;

            _vegaDbContext.Update(_mapper.Map<Vehicle>(vehicleDTO));

            int result;
            try
            {
                result = await _vegaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return NotFound("SaveChanges fail.");
            }

            if (result > 0)
            {
                return NoContent();
            }

            return NotFound("Could not update vehicle");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleDTO>> Delete(long Id)
        {
            int result;

            try
            {
                _vegaDbContext.Vehicles.Remove(
                    new Vehicle
                    {
                        Id = Id
                    }
                );

                result = await _vegaDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return NotFound("Delete fail");
            }

            if (result > 0)
            {
                return NoContent();
            }

            return NotFound($"Could not delete vehicle");
        }
    }
}