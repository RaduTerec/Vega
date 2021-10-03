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
    public class MakeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MakeController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<MakeDTO>> Get()
        {
            var makes = await _unitOfWork.Makes.GetWithRelated();

            return _mapper.Map<IEnumerable<MakeDTO>>(makes);
        }
    }
}