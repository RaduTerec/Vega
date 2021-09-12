using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Controllers.DataTransferObjects;
using Vega.Persistence;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MakeController : ControllerBase
    {
        private readonly VegaDbContext _vegaDbContext;
        private readonly IMapper _mapper;

        public MakeController(VegaDbContext context, IMapper mapper)
        {
            _vegaDbContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MakeDTO>> Get()
        {
            var makes = await _vegaDbContext.Makes.Include(f => f.Models).ToListAsync();

            return _mapper.Map<IEnumerable<MakeDTO>>(makes);
        }
    }
}