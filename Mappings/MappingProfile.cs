using AutoMapper;
using Vega.Models;
using Vega.Models.DataTransferObjects;

namespace Vega.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeDTO>();
            CreateMap<Model, ModelDTO>();
        }
    }
}