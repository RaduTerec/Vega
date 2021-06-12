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
    public class FeatureController : ControllerBase
    {
        private readonly VegaDbContext _vegaDbContext;
        private readonly IMapper _mapper;

        public FeatureController(VegaDbContext context, IMapper mapper)
        {
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