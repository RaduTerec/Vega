using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vega.Models;
using Vega.Models.DataTransferObjects;

namespace Vega.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IEnumerable<MakeDTO> GetMakes()  
        {
            var makes = _vegaDbContext.Makes.ToList();

            return _mapper.Map<IEnumerable<MakeDTO>>(makes);  
        }  
    }
}