using System.Linq;
using AutoMapper;
using Vega.Models;
using Vega.Models.DataTransferObjects;

namespace Vega.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<Make, MakeDTO>();
            CreateMap<Model, ModelDTO>();
            CreateMap<Feature, FeatureDTO>();
            CreateMap<Vehicle, VehicleDTO>();

            // API Resource to Domain
            CreateMap<VehicleDTO, Vehicle>()
            .ForMember(v => v.ContactName, opt => opt.MapFrom(vDto => vDto.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vDto => vDto.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vDto => vDto.Contact.Phone))
            .ForMember(v => v.Features, opt => opt.MapFrom(vDto => vDto.Features.Select(id => new Feature {Id = id})));
        }
    }
}