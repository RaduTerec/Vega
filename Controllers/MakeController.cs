using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Models;
using Vega.Models.DataTransferObjects;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MakeController : ControllerBase
    {
        private readonly VegaDbContext _vegaDbContext;
        private readonly ILogger<MakeController> _logger;
        private readonly IMapper _mapper;

        public MakeController(ILogger<MakeController> logger, VegaDbContext context, IMapper mapper)
        {
            _logger = logger;
            _vegaDbContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MakeDTO>> GetMakes()
        {
            var makes = await _vegaDbContext.Makes.Include(f => f.Models).ToListAsync();

            return _mapper.Map<IEnumerable<MakeDTO>>(makes);
        }
    }
}