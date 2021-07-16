using System.Linq;
using AutoMapper;
using Vega.Controllers.DataTransferObjects;
using Vega.Core.Models;

namespace Vega.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap(typeof(QueryResult<>),typeof(QueryResultDTO<>));
            CreateMap<Make, MakeDTO>();
            CreateMap<Make, KeyValuePairDTO>();
            CreateMap<Model, KeyValuePairDTO>();
            CreateMap<Feature, KeyValuePairDTO>();
            CreateMap<Vehicle, SaveVehicleDTO>()
                .ForMember(vDto => vDto.Contact, opt => opt.MapFrom(v => new ContactDTO
                {
                    Name = v.ContactName,
                    Email = v.ContactEmail,
                    Phone = v.ContactPhone
                }))
                .ForMember(vDto => vDto.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleDTO>()
                .ForMember(vDto => vDto.Contact, opt => opt.MapFrom(v => new ContactDTO
                {
                    Name = v.ContactName,
                    Email = v.ContactEmail,
                    Phone = v.ContactPhone
                }))
                .ForMember(vDto => vDto.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vDto => vDto.Features, opt => opt.MapFrom(v =>
                    v.Features.Select(vf => new KeyValuePairDTO { Id = vf.Feature.Id, Name = vf.Feature.Name })));


            // API to Domain
            CreateMap<VehicleQueryDTO, VehicleQuery>();
            CreateMap<SaveVehicleDTO, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vDto => vDto.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vDto => vDto.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vDto => vDto.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    // Remove unselected features
                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                    foreach (var feature in removedFeatures)
                    {
                        v.Features.Remove(feature);
                    }

                    // Add new features
                    var addedFeatures = vr.Features
                                            .Where(id => !v.Features.Any(f => f.FeatureId == id))
                                            .Select(id => new VehicleFeature { FeatureId = id }).ToList();
                    foreach (var feature in addedFeatures)
                    {
                        v.Features.Add(feature);
                    }
                });
        }
    }
}