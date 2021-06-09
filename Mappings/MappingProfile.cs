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
            CreateMap<Vehicle, VehicleDTO>()
                .ForMember(vDto => vDto.Contact, opt => opt.MapFrom(v => new ContactDTO
                {
                    Name = v.ContactName,
                    Email = v.ContactEmail,
                    Phone = v.ContactPhone
                }))
                .ForMember(vDto => vDto.Features, opt => opt.MapFrom(v => v.Features.Select(f => f.Id).ToArray()));

            // API to Domain
            CreateMap<VehicleDTO, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vDto => vDto.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vDto => vDto.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vDto => vDto.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore());
        }
    }
}