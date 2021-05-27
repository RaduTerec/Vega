using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vega.Models;

namespace Vega.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MakeController : ControllerBase
    {
        private VegaDbContext _vegaDbContext;
        private ILogger<MakeController> _logger;
  
        public MakeController(ILogger<MakeController> logger, VegaDbContext context)  
        {
            _logger = logger;
            _vegaDbContext = context;  
        }  
  
        [HttpGet("api/makes")]  
        public IEnumerable<Make> GetMakes()  
        {  
            return (_vegaDbContext.Makes.ToList());  
        }  
    }
}