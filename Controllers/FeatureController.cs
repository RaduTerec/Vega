using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Models.DataTransferObjects;
using Vega.Persistence;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly VegaDbContext _vegaDbContext;
        private readonly ILogger<FeatureController> _logger;
        private readonly IMapper _mapper;
  
        public FeatureController(ILogger<FeatureController> logger, VegaDbContext context, IMapper mapper)  
        {
            _logger = logger;
            _vegaDbContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<KeyValuePairDTO>> GetFeatures()
        {
            var features = await _vegaDbContext.Features.ToListAsync();

            return _mapper.Map<IEnumerable<KeyValuePairDTO>>(features);
        }
    }
}