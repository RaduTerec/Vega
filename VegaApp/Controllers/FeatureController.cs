using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Controllers.DataTransferObjects;
using Vega.Core;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FeatureController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<KeyValuePairDTO>> GetFeatures()
        {
            var features = await _unitOfWork.Features.GetAll();

            return _mapper.Map<IEnumerable<KeyValuePairDTO>>(features);
        }
    }
}