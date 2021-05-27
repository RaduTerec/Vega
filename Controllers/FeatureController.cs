using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vega.Models;

namespace Vega.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : ControllerBase
    {
        private VegaDbContext _vegaDbContext;
        private ILogger<FeatureController> _logger;
  
        public FeatureController(ILogger<FeatureController> logger, VegaDbContext context)  
        {
            _logger = logger;
            _vegaDbContext = context;  
        }  
  
        [HttpGet("api/features")]  
        public IEnumerable<Feature> GetMakes()  
        {  
            return (_vegaDbContext.Features.ToList());  
        }  
    }
}