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

            // API to Domain
            // TODO: this mapping is temporary.
            CreateMap<long, Feature>()
                .ForMember(f => f.Id, opt => opt.MapFrom(dest => dest));

            // Domain to API and API to Domain
            CreateMap<Vehicle, VehicleDTO>()
            .ForMember(vDto => vDto.Contact, opt => opt.MapFrom(v => new ContactDTO{
                Name = v.ContactName,
                Email = v.ContactEmail,
                Phone = v.ContactPhone
            }))
            .ForMember(vDto => vDto.Features, opt => opt.MapFrom(v => v.Features.Select(f => f.Id).ToArray()))
            .ReverseMap();
        }
    }
}